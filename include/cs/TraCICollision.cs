//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Eclipse.Sumo.Libtraci {

public class TraCICollision : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnBase;

  internal TraCICollision(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwnBase = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(TraCICollision obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~TraCICollision() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnBase) {
          swigCMemOwnBase = false;
          libtraciPINVOKE.delete_TraCICollision(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public string collider {
    set {
      libtraciPINVOKE.TraCICollision_collider_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCICollision_collider_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string victim {
    set {
      libtraciPINVOKE.TraCICollision_victim_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCICollision_victim_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string colliderType {
    set {
      libtraciPINVOKE.TraCICollision_colliderType_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCICollision_colliderType_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string victimType {
    set {
      libtraciPINVOKE.TraCICollision_victimType_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCICollision_victimType_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double colliderSpeed {
    set {
      libtraciPINVOKE.TraCICollision_colliderSpeed_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = libtraciPINVOKE.TraCICollision_colliderSpeed_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double victimSpeed {
    set {
      libtraciPINVOKE.TraCICollision_victimSpeed_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = libtraciPINVOKE.TraCICollision_victimSpeed_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string type {
    set {
      libtraciPINVOKE.TraCICollision_type_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCICollision_type_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string lane {
    set {
      libtraciPINVOKE.TraCICollision_lane_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCICollision_lane_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double pos {
    set {
      libtraciPINVOKE.TraCICollision_pos_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = libtraciPINVOKE.TraCICollision_pos_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string getString() {
    string ret = libtraciPINVOKE.TraCICollision_getString(swigCPtr);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public TraCICollision() : this(libtraciPINVOKE.new_TraCICollision(), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
