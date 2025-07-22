import torch
import torch.nn as nn
import torch.nn.functional as F
from torch_geometric.nn import GATConv, AttentionalAggregation

class GNNRoutingPolicy(nn.Module):
    def __init__(self, input_dim):
        super().__init__()
        hidden_dim = 64

        # Input dim adjusted for causal feature (+1)
        self.conv1 = GATConv(input_dim, hidden_dim, heads=2, concat=False)
        self.bn1 = nn.BatchNorm1d(hidden_dim)

        self.conv2 = GATConv(hidden_dim, hidden_dim, heads=2, concat=False)
        self.bn2 = nn.BatchNorm1d(hidden_dim)

        self.conv3 = GATConv(hidden_dim, hidden_dim, heads=2, concat=False)
        self.bn3 = nn.BatchNorm1d(hidden_dim)

        self.dropout = nn.Dropout(p=0.2)

        self.global_pool = AttentionalAggregation(gate_nn=nn.Sequential(
            nn.Linear(hidden_dim, 1),
            nn.Sigmoid()
        ))

        # Adjusted for causal feature
        self.mlp = nn.Sequential(
            nn.Linear(hidden_dim + 5, hidden_dim),  # +5 for context features including causal
            nn.ReLU(),
            nn.Linear(hidden_dim, hidden_dim)
        )

        self.policy_head = nn.Linear(hidden_dim, 1)
        self.value_head = nn.Linear(hidden_dim, 1)

    def forward(self, data, cur_idx=None):
        x, edge_index = data.x, data.edge_index
        context_feat = x[:, [9, 10, 11, 12, 13]]  # Include causal feature (index 13)

        x = F.relu(self.bn1(self.conv1(x, edge_index)))
        x = F.relu(self.bn2(self.conv2(x, edge_index)))
        x = F.relu(self.bn3(self.conv3(x, edge_index)))
        x = self.dropout(x)

        x = torch.cat([x, context_feat], dim=1)
        x = self.mlp(x)

        logits = self.policy_head(x)

        if cur_idx is None:
            value = self.value_head(x)
            return logits.squeeze(), value.squeeze()
        else:
            value = self.value_head(x[cur_idx])
            return logits.squeeze(), value.squeeze()