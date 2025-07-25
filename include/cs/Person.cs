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

public class Person : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Person(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Person obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(Person obj) {
    if (obj != null) {
      if (!obj.swigCMemOwn)
        throw new global::System.ApplicationException("Cannot release ownership as memory is not owned");
      global::System.Runtime.InteropServices.HandleRef ptr = obj.swigCPtr;
      obj.swigCMemOwn = false;
      obj.Dispose();
      return ptr;
    } else {
      return new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
    }
  }

  ~Person() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libtraciPINVOKE.delete_Person(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public static double getSpeed(string personID) {
    double ret = libtraciPINVOKE.Person_getSpeed(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static TraCIPosition getPosition(string personID, bool includeZ) {
    TraCIPosition ret = new TraCIPosition(libtraciPINVOKE.Person_getPosition__SWIG_0(personID, includeZ), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static TraCIPosition getPosition(string personID) {
    TraCIPosition ret = new TraCIPosition(libtraciPINVOKE.Person_getPosition__SWIG_1(personID), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static TraCIPosition getPosition3D(string personID) {
    TraCIPosition ret = new TraCIPosition(libtraciPINVOKE.Person_getPosition3D(personID), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string getRoadID(string personID) {
    string ret = libtraciPINVOKE.Person_getRoadID(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string getLaneID(string personID) {
    string ret = libtraciPINVOKE.Person_getLaneID(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string getTypeID(string personID) {
    string ret = libtraciPINVOKE.Person_getTypeID(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getWaitingTime(string personID) {
    double ret = libtraciPINVOKE.Person_getWaitingTime(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string getNextEdge(string personID) {
    string ret = libtraciPINVOKE.Person_getNextEdge(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string getVehicle(string personID) {
    string ret = libtraciPINVOKE.Person_getVehicle(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int getRemainingStages(string personID) {
    int ret = libtraciPINVOKE.Person_getRemainingStages(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static TraCIStage getStage(string personID, int nextStageIndex) {
    TraCIStage ret = new TraCIStage(libtraciPINVOKE.Person_getStage__SWIG_0(personID, nextStageIndex), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static TraCIStage getStage(string personID) {
    TraCIStage ret = new TraCIStage(libtraciPINVOKE.Person_getStage__SWIG_1(personID), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static StringVector getEdges(string personID, int nextStageIndex) {
    StringVector ret = new StringVector(libtraciPINVOKE.Person_getEdges__SWIG_0(personID, nextStageIndex), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static StringVector getEdges(string personID) {
    StringVector ret = new StringVector(libtraciPINVOKE.Person_getEdges__SWIG_1(personID), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getAngle(string personID) {
    double ret = libtraciPINVOKE.Person_getAngle(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getSlope(string personID) {
    double ret = libtraciPINVOKE.Person_getSlope(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getLanePosition(string personID) {
    double ret = libtraciPINVOKE.Person_getLanePosition(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getWalkingDistance(string personID, string edgeID, double pos, int laneIndex) {
    double ret = libtraciPINVOKE.Person_getWalkingDistance__SWIG_0(personID, edgeID, pos, laneIndex);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getWalkingDistance(string personID, string edgeID, double pos) {
    double ret = libtraciPINVOKE.Person_getWalkingDistance__SWIG_1(personID, edgeID, pos);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getWalkingDistance2D(string personID, double x, double y) {
    double ret = libtraciPINVOKE.Person_getWalkingDistance2D(personID, x, y);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static TraCIReservationVector getTaxiReservations(int onlyNew) {
    TraCIReservationVector ret = new TraCIReservationVector(libtraciPINVOKE.Person_getTaxiReservations__SWIG_0(onlyNew), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static TraCIReservationVector getTaxiReservations() {
    TraCIReservationVector ret = new TraCIReservationVector(libtraciPINVOKE.Person_getTaxiReservations__SWIG_1(), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string splitTaxiReservation(string reservationID, StringVector personIDs) {
    string ret = libtraciPINVOKE.Person_splitTaxiReservation(reservationID, StringVector.getCPtr(personIDs));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static StringVector getIDList() {
    StringVector ret = new StringVector(libtraciPINVOKE.Person_getIDList(), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int getIDCount() {
    int ret = libtraciPINVOKE.Person_getIDCount();
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string getParameter(string objectID, string key) {
    string ret = libtraciPINVOKE.Person_getParameter(objectID, key);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static StringStringPair getParameterWithKey(string objectID, string key) {
    StringStringPair ret = new StringStringPair(libtraciPINVOKE.Person_getParameterWithKey(objectID, key), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void setParameter(string objectID, string key, string value) {
    libtraciPINVOKE.Person_setParameter(objectID, key, value);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static double getLength(string typeID) {
    double ret = libtraciPINVOKE.Person_getLength(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getMaxSpeed(string typeID) {
    double ret = libtraciPINVOKE.Person_getMaxSpeed(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string getVehicleClass(string typeID) {
    string ret = libtraciPINVOKE.Person_getVehicleClass(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getSpeedFactor(string typeID) {
    double ret = libtraciPINVOKE.Person_getSpeedFactor(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getAccel(string typeID) {
    double ret = libtraciPINVOKE.Person_getAccel(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getDecel(string typeID) {
    double ret = libtraciPINVOKE.Person_getDecel(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getEmergencyDecel(string typeID) {
    double ret = libtraciPINVOKE.Person_getEmergencyDecel(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getApparentDecel(string typeID) {
    double ret = libtraciPINVOKE.Person_getApparentDecel(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getImperfection(string typeID) {
    double ret = libtraciPINVOKE.Person_getImperfection(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getTau(string typeID) {
    double ret = libtraciPINVOKE.Person_getTau(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string getEmissionClass(string typeID) {
    string ret = libtraciPINVOKE.Person_getEmissionClass(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string getShapeClass(string typeID) {
    string ret = libtraciPINVOKE.Person_getShapeClass(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getMinGap(string typeID) {
    double ret = libtraciPINVOKE.Person_getMinGap(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getWidth(string typeID) {
    double ret = libtraciPINVOKE.Person_getWidth(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getHeight(string typeID) {
    double ret = libtraciPINVOKE.Person_getHeight(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getMass(string typeID) {
    double ret = libtraciPINVOKE.Person_getMass(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static TraCIColor getColor(string typeID) {
    TraCIColor ret = new TraCIColor(libtraciPINVOKE.Person_getColor(typeID), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getMinGapLat(string typeID) {
    double ret = libtraciPINVOKE.Person_getMinGapLat(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getMaxSpeedLat(string typeID) {
    double ret = libtraciPINVOKE.Person_getMaxSpeedLat(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string getLateralAlignment(string typeID) {
    string ret = libtraciPINVOKE.Person_getLateralAlignment(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int getPersonCapacity(string typeID) {
    int ret = libtraciPINVOKE.Person_getPersonCapacity(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getActionStepLength(string typeID) {
    double ret = libtraciPINVOKE.Person_getActionStepLength(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getSpeedDeviation(string typeID) {
    double ret = libtraciPINVOKE.Person_getSpeedDeviation(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getBoardingDuration(string typeID) {
    double ret = libtraciPINVOKE.Person_getBoardingDuration(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static double getImpatience(string typeID) {
    double ret = libtraciPINVOKE.Person_getImpatience(typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void add(string personID, string edgeID, double pos, double depart, string typeID) {
    libtraciPINVOKE.Person_add__SWIG_0(personID, edgeID, pos, depart, typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void add(string personID, string edgeID, double pos, double depart) {
    libtraciPINVOKE.Person_add__SWIG_1(personID, edgeID, pos, depart);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void add(string personID, string edgeID, double pos) {
    libtraciPINVOKE.Person_add__SWIG_2(personID, edgeID, pos);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void appendStage(string personID, TraCIStage stage) {
    libtraciPINVOKE.Person_appendStage(personID, TraCIStage.getCPtr(stage));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void replaceStage(string personID, int stageIndex, TraCIStage stage) {
    libtraciPINVOKE.Person_replaceStage(personID, stageIndex, TraCIStage.getCPtr(stage));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void appendWaitingStage(string personID, double duration, string description, string stopID) {
    libtraciPINVOKE.Person_appendWaitingStage__SWIG_0(personID, duration, description, stopID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void appendWaitingStage(string personID, double duration, string description) {
    libtraciPINVOKE.Person_appendWaitingStage__SWIG_1(personID, duration, description);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void appendWaitingStage(string personID, double duration) {
    libtraciPINVOKE.Person_appendWaitingStage__SWIG_2(personID, duration);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void appendWalkingStage(string personID, StringVector edges, double arrivalPos, double duration, double speed, string stopID) {
    libtraciPINVOKE.Person_appendWalkingStage__SWIG_0(personID, StringVector.getCPtr(edges), arrivalPos, duration, speed, stopID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void appendWalkingStage(string personID, StringVector edges, double arrivalPos, double duration, double speed) {
    libtraciPINVOKE.Person_appendWalkingStage__SWIG_1(personID, StringVector.getCPtr(edges), arrivalPos, duration, speed);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void appendWalkingStage(string personID, StringVector edges, double arrivalPos, double duration) {
    libtraciPINVOKE.Person_appendWalkingStage__SWIG_2(personID, StringVector.getCPtr(edges), arrivalPos, duration);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void appendWalkingStage(string personID, StringVector edges, double arrivalPos) {
    libtraciPINVOKE.Person_appendWalkingStage__SWIG_3(personID, StringVector.getCPtr(edges), arrivalPos);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void appendDrivingStage(string personID, string toEdge, string lines, string stopID) {
    libtraciPINVOKE.Person_appendDrivingStage__SWIG_0(personID, toEdge, lines, stopID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void appendDrivingStage(string personID, string toEdge, string lines) {
    libtraciPINVOKE.Person_appendDrivingStage__SWIG_1(personID, toEdge, lines);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void removeStage(string personID, int nextStageIndex) {
    libtraciPINVOKE.Person_removeStage(personID, nextStageIndex);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void rerouteTraveltime(string personID) {
    libtraciPINVOKE.Person_rerouteTraveltime(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void moveTo(string personID, string laneID, double pos, double posLat) {
    libtraciPINVOKE.Person_moveTo__SWIG_0(personID, laneID, pos, posLat);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void moveTo(string personID, string laneID, double pos) {
    libtraciPINVOKE.Person_moveTo__SWIG_1(personID, laneID, pos);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void moveToXY(string personID, string edgeID, double x, double y, double angle, int keepRoute, double matchThreshold) {
    libtraciPINVOKE.Person_moveToXY__SWIG_0(personID, edgeID, x, y, angle, keepRoute, matchThreshold);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void moveToXY(string personID, string edgeID, double x, double y, double angle, int keepRoute) {
    libtraciPINVOKE.Person_moveToXY__SWIG_1(personID, edgeID, x, y, angle, keepRoute);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void moveToXY(string personID, string edgeID, double x, double y, double angle) {
    libtraciPINVOKE.Person_moveToXY__SWIG_2(personID, edgeID, x, y, angle);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void moveToXY(string personID, string edgeID, double x, double y) {
    libtraciPINVOKE.Person_moveToXY__SWIG_3(personID, edgeID, x, y);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void remove(string personID, char reason) {
    libtraciPINVOKE.Person_remove__SWIG_0(personID, reason);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void remove(string personID) {
    libtraciPINVOKE.Person_remove__SWIG_1(personID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setSpeed(string personID, double speed) {
    libtraciPINVOKE.Person_setSpeed(personID, speed);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setType(string personID, string typeID) {
    libtraciPINVOKE.Person_setType(personID, typeID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setLength(string typeID, double length) {
    libtraciPINVOKE.Person_setLength(typeID, length);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setMaxSpeed(string typeID, double speed) {
    libtraciPINVOKE.Person_setMaxSpeed(typeID, speed);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setVehicleClass(string typeID, string clazz) {
    libtraciPINVOKE.Person_setVehicleClass(typeID, clazz);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setSpeedFactor(string typeID, double factor) {
    libtraciPINVOKE.Person_setSpeedFactor(typeID, factor);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setAccel(string typeID, double accel) {
    libtraciPINVOKE.Person_setAccel(typeID, accel);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setDecel(string typeID, double decel) {
    libtraciPINVOKE.Person_setDecel(typeID, decel);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setEmergencyDecel(string typeID, double decel) {
    libtraciPINVOKE.Person_setEmergencyDecel(typeID, decel);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setApparentDecel(string typeID, double decel) {
    libtraciPINVOKE.Person_setApparentDecel(typeID, decel);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setImperfection(string typeID, double imperfection) {
    libtraciPINVOKE.Person_setImperfection(typeID, imperfection);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setTau(string typeID, double tau) {
    libtraciPINVOKE.Person_setTau(typeID, tau);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setEmissionClass(string typeID, string clazz) {
    libtraciPINVOKE.Person_setEmissionClass(typeID, clazz);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setShapeClass(string typeID, string shapeClass) {
    libtraciPINVOKE.Person_setShapeClass(typeID, shapeClass);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setWidth(string typeID, double width) {
    libtraciPINVOKE.Person_setWidth(typeID, width);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setHeight(string typeID, double height) {
    libtraciPINVOKE.Person_setHeight(typeID, height);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setMass(string typeID, double mass) {
    libtraciPINVOKE.Person_setMass(typeID, mass);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setColor(string typeID, TraCIColor color) {
    libtraciPINVOKE.Person_setColor(typeID, TraCIColor.getCPtr(color));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setMinGap(string typeID, double minGap) {
    libtraciPINVOKE.Person_setMinGap(typeID, minGap);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setMinGapLat(string typeID, double minGapLat) {
    libtraciPINVOKE.Person_setMinGapLat(typeID, minGapLat);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setMaxSpeedLat(string typeID, double speed) {
    libtraciPINVOKE.Person_setMaxSpeedLat(typeID, speed);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setLateralAlignment(string typeID, string latAlignment) {
    libtraciPINVOKE.Person_setLateralAlignment(typeID, latAlignment);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setActionStepLength(string typeID, double actionStepLength, bool resetActionOffset) {
    libtraciPINVOKE.Person_setActionStepLength__SWIG_0(typeID, actionStepLength, resetActionOffset);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setActionStepLength(string typeID, double actionStepLength) {
    libtraciPINVOKE.Person_setActionStepLength__SWIG_1(typeID, actionStepLength);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setBoardingDuration(string typeID, double boardingDuration) {
    libtraciPINVOKE.Person_setBoardingDuration(typeID, boardingDuration);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void setImpatience(string typeID, double impatience) {
    libtraciPINVOKE.Person_setImpatience(typeID, impatience);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void subscribe(string objectID, IntVector varIDs, double begin, double end, TraCIResults parameters) {
    libtraciPINVOKE.Person_subscribe__SWIG_0(objectID, IntVector.getCPtr(varIDs), begin, end, TraCIResults.getCPtr(parameters));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void subscribe(string objectID, IntVector varIDs, double begin, double end) {
    libtraciPINVOKE.Person_subscribe__SWIG_1(objectID, IntVector.getCPtr(varIDs), begin, end);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void subscribe(string objectID, IntVector varIDs, double begin) {
    libtraciPINVOKE.Person_subscribe__SWIG_2(objectID, IntVector.getCPtr(varIDs), begin);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void subscribe(string objectID, IntVector varIDs) {
    libtraciPINVOKE.Person_subscribe__SWIG_3(objectID, IntVector.getCPtr(varIDs));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void subscribe(string objectID) {
    libtraciPINVOKE.Person_subscribe__SWIG_4(objectID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void unsubscribe(string objectID) {
    libtraciPINVOKE.Person_unsubscribe(objectID);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void subscribeContext(string objectID, int domain, double dist, IntVector varIDs, double begin, double end, TraCIResults parameters) {
    libtraciPINVOKE.Person_subscribeContext__SWIG_0(objectID, domain, dist, IntVector.getCPtr(varIDs), begin, end, TraCIResults.getCPtr(parameters));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void subscribeContext(string objectID, int domain, double dist, IntVector varIDs, double begin, double end) {
    libtraciPINVOKE.Person_subscribeContext__SWIG_1(objectID, domain, dist, IntVector.getCPtr(varIDs), begin, end);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void subscribeContext(string objectID, int domain, double dist, IntVector varIDs, double begin) {
    libtraciPINVOKE.Person_subscribeContext__SWIG_2(objectID, domain, dist, IntVector.getCPtr(varIDs), begin);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void subscribeContext(string objectID, int domain, double dist, IntVector varIDs) {
    libtraciPINVOKE.Person_subscribeContext__SWIG_3(objectID, domain, dist, IntVector.getCPtr(varIDs));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void subscribeContext(string objectID, int domain, double dist) {
    libtraciPINVOKE.Person_subscribeContext__SWIG_4(objectID, domain, dist);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void unsubscribeContext(string objectID, int domain, double dist) {
    libtraciPINVOKE.Person_unsubscribeContext(objectID, domain, dist);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static SubscriptionResults getAllSubscriptionResults() {
    SubscriptionResults ret = new SubscriptionResults(libtraciPINVOKE.Person_getAllSubscriptionResults(), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static TraCIResults getSubscriptionResults(string objectID) {
    TraCIResults ret = new TraCIResults(libtraciPINVOKE.Person_getSubscriptionResults(objectID), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ContextSubscriptionResults getAllContextSubscriptionResults() {
    ContextSubscriptionResults ret = new ContextSubscriptionResults(libtraciPINVOKE.Person_getAllContextSubscriptionResults(), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static SubscriptionResults getContextSubscriptionResults(string objectID) {
    SubscriptionResults ret = new SubscriptionResults(libtraciPINVOKE.Person_getContextSubscriptionResults(objectID), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void subscribeParameterWithKey(string objectID, string key, double beginTime, double endTime) {
    libtraciPINVOKE.Person_subscribeParameterWithKey__SWIG_0(objectID, key, beginTime, endTime);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void subscribeParameterWithKey(string objectID, string key, double beginTime) {
    libtraciPINVOKE.Person_subscribeParameterWithKey__SWIG_1(objectID, key, beginTime);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void subscribeParameterWithKey(string objectID, string key) {
    libtraciPINVOKE.Person_subscribeParameterWithKey__SWIG_2(objectID, key);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static int DOMAIN_ID {
    get {
      int ret = libtraciPINVOKE.Person_DOMAIN_ID_get();
      if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public static int domainID() {
    int ret = libtraciPINVOKE.Person_domainID();
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
