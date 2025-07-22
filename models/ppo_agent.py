import torch
import torch.nn as nn
import torch.optim as optim
import numpy as np
from collections import deque
from torch_geometric.data import Batch
from ray.rllib.agents.qmix import QMixTrainer
from ray.rllib.agents.ppo import PPOTrainer
import ray
from opacus import PrivacyEngine

class PPOAgent:
    def __init__(self, model, lr=3e-4, gamma=0.99, gae_lambda=0.95,
                 clip_eps=0.2, entropy_coef=0.005, value_coef=0.5,
                 batch_size=8, update_epochs=10, max_grad_norm=0.5, max_nodes_per_batch=500):
        self.model = model
        self.device = model.device if hasattr(model, "device") else torch.device("cuda" if torch.cuda.is_available() else "cpu")
        self.model.to(self.device)
        self.gamma = gamma
        self.gae_lambda = gae_lambda
        self.clip_eps = clip_eps
        self.entropy_coef = entropy_coef
        self.value_coef = value_coef
        self.batch_size = batch_size
        self.update_epochs = update_epochs
        self.max_grad_norm = max_grad_norm
        self.max_nodes_per_batch = max_nodes_per_batch

        # Initialize RLlib trainer for QMIX (cooperative MARL)
        self.trainer_config = {
            "model": {"custom_model": self.model},  # GNNRoutingPolicy
            "num_workers": 4,  # Parallel environments
            "framework": "torch",
            "train_batch_size": 256,
            "multiagent": {
                "policies": {"shared": (None, None, None, {})},  # Will be set by env
                "policy_mapping_fn": lambda agent_id: "shared",
            },
        }
        self.trainer = QMixTrainer(config=self.trainer_config)
        
        # Differential Privacy
        self.privacy_engine = PrivacyEngine(
            self.model,
            sample_rate=0.01,
            noise_multiplier=0.8,
            max_grad_norm=max_grad_norm
        )
        self.privacy_engine.attach(self.trainer.optimizer)

        # Buffer
        self.reset_buffer()

    def reset_buffer(self):
        self.buffer = {
            'observations': [],
            'actions': [],
            'log_probs': [],
            'rewards': [],
            'dones': [],
            'values': [],
        }

    def store(self, obs, action, log_prob, reward, done, value):
        self.buffer['observations'].append(obs.cpu())  # Move to CPU
        self.buffer['actions'].append(action)
        self.buffer['log_probs'].append(float(log_prob))  # Detach
        self.buffer['rewards'].append(reward)
        self.buffer['dones'].append(done)
        self.buffer['values'].append(float(value))  # Detach

    def compute_returns_and_advantages(self, last_value):
        rewards = self.buffer['rewards']
        dones = self.buffer['dones']
        values = self.buffer['values'] + [last_value]

        returns = []
        advantages = []
        gae = 0
        for step in reversed(range(len(rewards))):
            delta = rewards[step] + self.gamma * values[step + 1] * (1 - dones[step]) - values[step]
            gae = delta + self.gamma * self.gae_lambda * (1 - dones[step]) * gae
            advantages.insert(0, gae)
            returns.insert(0, gae + values[step])
        return returns, advantages

    def update(self, last_value):
        returns, adv = self.compute_returns_and_advantages(last_value)
        actions = torch.tensor(self.buffer['actions'], device=self.device)
        log_probs_old = torch.tensor(self.buffer['log_probs'], device=self.device)
        returns = torch.tensor(returns, device=self.device)
        adv = torch.tensor(adv, device=self.device)

        self.trainer.optimizer.zero_grad()
        batch_nodes = 0
        mini_obs, mini_idx = [], []

        for i, obs in enumerate(self.buffer['observations']):
            mini_obs.append(obs)
            mini_idx.append(i)
            batch_nodes += obs.num_nodes

            if batch_nodes >= self.max_nodes_per_batch or i == len(self.buffer['observations']) - 1:
                mb = Batch.from_data_list(mini_obs).to(self.device)
                logits, _ = self.model(mb)

                loss_sum = 0.0
                offset = 0
                for j, data_j in zip(mini_idx, mini_obs):
                    n = data_j.num_nodes
                    logits_j = logits[offset:offset + n]
                    offset += n

                    logit_act = logits_j[actions[j]]
                    log_prob = logit_act - torch.logsumexp(logits_j, dim=0)
                    ratio = torch.exp(log_prob - log_probs_old[j])
                    surr1 = ratio * adv[j]
                    surr2 = torch.clamp(ratio, 1 - self.clip_eps, 1 + self.clip_eps) * adv[j]
                    entropy = -(logits_j.softmax(0) * logits_j.log_softmax(0)).sum()

                    loss_sum += (-torch.min(surr1, surr2) - self.entropy_coef * entropy)

                (loss_sum / len(mini_obs)).backward()

                # Gradient pruning
                for param in self.model.parameters():
                    if param.grad is not None:
                        threshold = torch.quantile(param.grad.abs(), 0.9)
                        mask = param.grad.abs() >= threshold
                        param.grad.data.mul_(mask.float())

                # reset mini-batch holders
                mini_obs, mini_idx = [], []
                batch_nodes = 0
                del mb, logits

        torch.nn.utils.clip_grad_norm_(self.model.parameters(), self.max_grad_norm)
        self.trainer.optimizer.step()
        self.reset_buffer()
        torch.cuda.empty_cache()

    def train_with_rllib(self, env, num_iterations=100):
        """Train using RLlib QMIX"""
        self.trainer_config["env"] = env
        self.trainer = QMixTrainer(config=self.trainer_config)
        for _ in range(num_iterations):
            result = self.trainer.train()
            print(f"Iteration: {result['training_iteration']}, Reward: {result['episode_reward_mean']}")
        return result