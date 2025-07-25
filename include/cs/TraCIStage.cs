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

public class TraCIStage : TraCIResult {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal TraCIStage(global::System.IntPtr cPtr, bool cMemoryOwn) : base(libtraciPINVOKE.TraCIStage_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(TraCIStage obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          libtraciPINVOKE.delete_TraCIStage(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public TraCIStage(int type, string vType, string line, string destStop, StringVector edges, double travelTime, double cost, double length, string intended, double depart, double departPos, double arrivalPos, string description) : this(libtraciPINVOKE.new_TraCIStage__SWIG_0(type, vType, line, destStop, StringVector.getCPtr(edges), travelTime, cost, length, intended, depart, departPos, arrivalPos, description), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage(int type, string vType, string line, string destStop, StringVector edges, double travelTime, double cost, double length, string intended, double depart, double departPos, double arrivalPos) : this(libtraciPINVOKE.new_TraCIStage__SWIG_1(type, vType, line, destStop, StringVector.getCPtr(edges), travelTime, cost, length, intended, depart, departPos, arrivalPos), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage(int type, string vType, string line, string destStop, StringVector edges, double travelTime, double cost, double length, string intended, double depart, double departPos) : this(libtraciPINVOKE.new_TraCIStage__SWIG_2(type, vType, line, destStop, StringVector.getCPtr(edges), travelTime, cost, length, intended, depart, departPos), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage(int type, string vType, string line, string destStop, StringVector edges, double travelTime, double cost, double length, string intended, double depart) : this(libtraciPINVOKE.new_TraCIStage__SWIG_3(type, vType, line, destStop, StringVector.getCPtr(edges), travelTime, cost, length, intended, depart), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage(int type, string vType, string line, string destStop, StringVector edges, double travelTime, double cost, double length, string intended) : this(libtraciPINVOKE.new_TraCIStage__SWIG_4(type, vType, line, destStop, StringVector.getCPtr(edges), travelTime, cost, length, intended), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage(int type, string vType, string line, string destStop, StringVector edges, double travelTime, double cost, double length) : this(libtraciPINVOKE.new_TraCIStage__SWIG_5(type, vType, line, destStop, StringVector.getCPtr(edges), travelTime, cost, length), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage(int type, string vType, string line, string destStop, StringVector edges, double travelTime, double cost) : this(libtraciPINVOKE.new_TraCIStage__SWIG_6(type, vType, line, destStop, StringVector.getCPtr(edges), travelTime, cost), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage(int type, string vType, string line, string destStop, StringVector edges, double travelTime) : this(libtraciPINVOKE.new_TraCIStage__SWIG_7(type, vType, line, destStop, StringVector.getCPtr(edges), travelTime), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage(int type, string vType, string line, string destStop, StringVector edges) : this(libtraciPINVOKE.new_TraCIStage__SWIG_8(type, vType, line, destStop, StringVector.getCPtr(edges)), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage(int type, string vType, string line, string destStop) : this(libtraciPINVOKE.new_TraCIStage__SWIG_9(type, vType, line, destStop), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage(int type, string vType, string line) : this(libtraciPINVOKE.new_TraCIStage__SWIG_10(type, vType, line), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage(int type, string vType) : this(libtraciPINVOKE.new_TraCIStage__SWIG_11(type, vType), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage(int type) : this(libtraciPINVOKE.new_TraCIStage__SWIG_12(type), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIStage() : this(libtraciPINVOKE.new_TraCIStage__SWIG_13(), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public int type {
    set {
      libtraciPINVOKE.TraCIStage_type_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = libtraciPINVOKE.TraCIStage_type_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string vType {
    set {
      libtraciPINVOKE.TraCIStage_vType_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCIStage_vType_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string line {
    set {
      libtraciPINVOKE.TraCIStage_line_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCIStage_line_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string destStop {
    set {
      libtraciPINVOKE.TraCIStage_destStop_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCIStage_destStop_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public StringVector edges {
    set {
      libtraciPINVOKE.TraCIStage_edges_set(swigCPtr, StringVector.getCPtr(value));
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = libtraciPINVOKE.TraCIStage_edges_get(swigCPtr);
      StringVector ret = (cPtr == global::System.IntPtr.Zero) ? null : new StringVector(cPtr, false);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double travelTime {
    set {
      libtraciPINVOKE.TraCIStage_travelTime_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = libtraciPINVOKE.TraCIStage_travelTime_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double cost {
    set {
      libtraciPINVOKE.TraCIStage_cost_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = libtraciPINVOKE.TraCIStage_cost_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double length {
    set {
      libtraciPINVOKE.TraCIStage_length_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = libtraciPINVOKE.TraCIStage_length_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string intended {
    set {
      libtraciPINVOKE.TraCIStage_intended_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCIStage_intended_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double depart {
    set {
      libtraciPINVOKE.TraCIStage_depart_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = libtraciPINVOKE.TraCIStage_depart_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double departPos {
    set {
      libtraciPINVOKE.TraCIStage_departPos_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = libtraciPINVOKE.TraCIStage_departPos_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double arrivalPos {
    set {
      libtraciPINVOKE.TraCIStage_arrivalPos_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = libtraciPINVOKE.TraCIStage_arrivalPos_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string description {
    set {
      libtraciPINVOKE.TraCIStage_description_set(swigCPtr, value);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = libtraciPINVOKE.TraCIStage_description_get(swigCPtr);
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

}

}
