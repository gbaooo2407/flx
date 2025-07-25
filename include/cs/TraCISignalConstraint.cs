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

public class TraCISignalConstraint : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnBase;

  internal TraCISignalConstraint(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwnBase = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(TraCISignalConstraint obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~TraCISignalConstraint() {
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
          libtraciPINVOKE.delete_TraCISignalConstraint(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public string signalId {
    set {
      libtraciPINVOKE.TraCISignalConstraint_signalId_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCISignalConstraint_signalId_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string tripId {
    set {
      libtraciPINVOKE.TraCISignalConstraint_tripId_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCISignalConstraint_tripId_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string foeId {
    set {
      libtraciPINVOKE.TraCISignalConstraint_foeId_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCISignalConstraint_foeId_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string foeSignal {
    set {
      libtraciPINVOKE.TraCISignalConstraint_foeSignal_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCISignalConstraint_foeSignal_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int limit {
    set {
      libtraciPINVOKE.TraCISignalConstraint_limit_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = libtraciPINVOKE.TraCISignalConstraint_limit_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int type {
    set {
      libtraciPINVOKE.TraCISignalConstraint_type_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = libtraciPINVOKE.TraCISignalConstraint_type_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public bool mustWait {
    set {
      libtraciPINVOKE.TraCISignalConstraint_mustWait_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      bool ret = libtraciPINVOKE.TraCISignalConstraint_mustWait_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public bool active {
    set {
      libtraciPINVOKE.TraCISignalConstraint_active_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      bool ret = libtraciPINVOKE.TraCISignalConstraint_active_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public StringStringMap param {
    set {
      libtraciPINVOKE.TraCISignalConstraint_param_set(swigCPtr, StringStringMap.getCPtr(value));
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = libtraciPINVOKE.TraCISignalConstraint_param_get(swigCPtr);
      StringStringMap ret = (cPtr == global::System.IntPtr.Zero) ? null : new StringStringMap(cPtr, false);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string getString() {
    string ret = libtraciPINVOKE.TraCISignalConstraint_getString(swigCPtr);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public TraCISignalConstraint() : this(libtraciPINVOKE.new_TraCISignalConstraint(), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
