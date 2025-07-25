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

public class TraCIJunctionFoeVector : global::System.IDisposable, global::System.Collections.IEnumerable, global::System.Collections.Generic.IEnumerable<TraCIJunctionFoe>
 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal TraCIJunctionFoeVector(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(TraCIJunctionFoeVector obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(TraCIJunctionFoeVector obj) {
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

  ~TraCIJunctionFoeVector() {
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
          libtraciPINVOKE.delete_TraCIJunctionFoeVector(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public TraCIJunctionFoeVector(global::System.Collections.IEnumerable c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (TraCIJunctionFoe element in c) {
      this.Add(element);
    }
  }

  public TraCIJunctionFoeVector(global::System.Collections.Generic.IEnumerable<TraCIJunctionFoe> c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (TraCIJunctionFoe element in c) {
      this.Add(element);
    }
  }

  public bool IsFixedSize {
    get {
      return false;
    }
  }

  public bool IsReadOnly {
    get {
      return false;
    }
  }

  public TraCIJunctionFoe this[int index]  {
    get {
      return getitem(index);
    }
    set {
      setitem(index, value);
    }
  }

  public int Capacity {
    get {
      return (int)capacity();
    }
    set {
      if (value < 0 || (uint)value < size())
        throw new global::System.ArgumentOutOfRangeException("Capacity");
      reserve((uint)value);
    }
  }

  public bool IsEmpty {
    get {
      return empty();
    }
  }

  public int Count {
    get {
      return (int)size();
    }
  }

  public bool IsSynchronized {
    get {
      return false;
    }
  }

  public void CopyTo(TraCIJunctionFoe[] array)
  {
    CopyTo(0, array, 0, this.Count);
  }

  public void CopyTo(TraCIJunctionFoe[] array, int arrayIndex)
  {
    CopyTo(0, array, arrayIndex, this.Count);
  }

  public void CopyTo(int index, TraCIJunctionFoe[] array, int arrayIndex, int count)
  {
    if (array == null)
      throw new global::System.ArgumentNullException("array");
    if (index < 0)
      throw new global::System.ArgumentOutOfRangeException("index", "Value is less than zero");
    if (arrayIndex < 0)
      throw new global::System.ArgumentOutOfRangeException("arrayIndex", "Value is less than zero");
    if (count < 0)
      throw new global::System.ArgumentOutOfRangeException("count", "Value is less than zero");
    if (array.Rank > 1)
      throw new global::System.ArgumentException("Multi dimensional array.", "array");
    if (index+count > this.Count || arrayIndex+count > array.Length)
      throw new global::System.ArgumentException("Number of elements to copy is too large.");
    for (int i=0; i<count; i++)
      array.SetValue(getitemcopy(index+i), arrayIndex+i);
  }

  public TraCIJunctionFoe[] ToArray() {
    TraCIJunctionFoe[] array = new TraCIJunctionFoe[this.Count];
    this.CopyTo(array);
    return array;
  }

  global::System.Collections.Generic.IEnumerator<TraCIJunctionFoe> global::System.Collections.Generic.IEnumerable<TraCIJunctionFoe>.GetEnumerator() {
    return new TraCIJunctionFoeVectorEnumerator(this);
  }

  global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() {
    return new TraCIJunctionFoeVectorEnumerator(this);
  }

  public TraCIJunctionFoeVectorEnumerator GetEnumerator() {
    return new TraCIJunctionFoeVectorEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class TraCIJunctionFoeVectorEnumerator : global::System.Collections.IEnumerator
    , global::System.Collections.Generic.IEnumerator<TraCIJunctionFoe>
  {
    private TraCIJunctionFoeVector collectionRef;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public TraCIJunctionFoeVectorEnumerator(TraCIJunctionFoeVector collection) {
      collectionRef = collection;
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public TraCIJunctionFoe Current {
      get {
        if (currentIndex == -1)
          throw new global::System.InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new global::System.InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new global::System.InvalidOperationException("Collection modified.");
        return (TraCIJunctionFoe)currentObject;
      }
    }

    // Type-unsafe IEnumerator.Current
    object global::System.Collections.IEnumerator.Current {
      get {
        return Current;
      }
    }

    public bool MoveNext() {
      int size = collectionRef.Count;
      bool moveOkay = (currentIndex+1 < size) && (size == currentSize);
      if (moveOkay) {
        currentIndex++;
        currentObject = collectionRef[currentIndex];
      } else {
        currentObject = null;
      }
      return moveOkay;
    }

    public void Reset() {
      currentIndex = -1;
      currentObject = null;
      if (collectionRef.Count != currentSize) {
        throw new global::System.InvalidOperationException("Collection modified.");
      }
    }

    public void Dispose() {
        currentIndex = -1;
        currentObject = null;
    }
  }

  public TraCIJunctionFoeVector() : this(libtraciPINVOKE.new_TraCIJunctionFoeVector__SWIG_0(), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIJunctionFoeVector(TraCIJunctionFoeVector other) : this(libtraciPINVOKE.new_TraCIJunctionFoeVector__SWIG_1(TraCIJunctionFoeVector.getCPtr(other)), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Clear() {
    libtraciPINVOKE.TraCIJunctionFoeVector_Clear(swigCPtr);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Add(TraCIJunctionFoe x) {
    libtraciPINVOKE.TraCIJunctionFoeVector_Add(swigCPtr, TraCIJunctionFoe.getCPtr(x));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  private uint size() {
    uint ret = libtraciPINVOKE.TraCIJunctionFoeVector_size(swigCPtr);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private bool empty() {
    bool ret = libtraciPINVOKE.TraCIJunctionFoeVector_empty(swigCPtr);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private uint capacity() {
    uint ret = libtraciPINVOKE.TraCIJunctionFoeVector_capacity(swigCPtr);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void reserve(uint n) {
    libtraciPINVOKE.TraCIJunctionFoeVector_reserve(swigCPtr, n);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIJunctionFoeVector(int capacity) : this(libtraciPINVOKE.new_TraCIJunctionFoeVector__SWIG_2(capacity), true) {
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  private TraCIJunctionFoe getitemcopy(int index) {
    TraCIJunctionFoe ret = new TraCIJunctionFoe(libtraciPINVOKE.TraCIJunctionFoeVector_getitemcopy(swigCPtr, index), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private TraCIJunctionFoe getitem(int index) {
    TraCIJunctionFoe ret = new TraCIJunctionFoe(libtraciPINVOKE.TraCIJunctionFoeVector_getitem(swigCPtr, index), true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(int index, TraCIJunctionFoe val) {
    libtraciPINVOKE.TraCIJunctionFoeVector_setitem(swigCPtr, index, TraCIJunctionFoe.getCPtr(val));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddRange(TraCIJunctionFoeVector values) {
    libtraciPINVOKE.TraCIJunctionFoeVector_AddRange(swigCPtr, TraCIJunctionFoeVector.getCPtr(values));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public TraCIJunctionFoeVector GetRange(int index, int count) {
    global::System.IntPtr cPtr = libtraciPINVOKE.TraCIJunctionFoeVector_GetRange(swigCPtr, index, count);
    TraCIJunctionFoeVector ret = (cPtr == global::System.IntPtr.Zero) ? null : new TraCIJunctionFoeVector(cPtr, true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Insert(int index, TraCIJunctionFoe x) {
    libtraciPINVOKE.TraCIJunctionFoeVector_Insert(swigCPtr, index, TraCIJunctionFoe.getCPtr(x));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public void InsertRange(int index, TraCIJunctionFoeVector values) {
    libtraciPINVOKE.TraCIJunctionFoeVector_InsertRange(swigCPtr, index, TraCIJunctionFoeVector.getCPtr(values));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAt(int index) {
    libtraciPINVOKE.TraCIJunctionFoeVector_RemoveAt(swigCPtr, index);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveRange(int index, int count) {
    libtraciPINVOKE.TraCIJunctionFoeVector_RemoveRange(swigCPtr, index, count);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public static TraCIJunctionFoeVector Repeat(TraCIJunctionFoe value, int count) {
    global::System.IntPtr cPtr = libtraciPINVOKE.TraCIJunctionFoeVector_Repeat(TraCIJunctionFoe.getCPtr(value), count);
    TraCIJunctionFoeVector ret = (cPtr == global::System.IntPtr.Zero) ? null : new TraCIJunctionFoeVector(cPtr, true);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reverse() {
    libtraciPINVOKE.TraCIJunctionFoeVector_Reverse__SWIG_0(swigCPtr);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Reverse(int index, int count) {
    libtraciPINVOKE.TraCIJunctionFoeVector_Reverse__SWIG_1(swigCPtr, index, count);
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRange(int index, TraCIJunctionFoeVector values) {
    libtraciPINVOKE.TraCIJunctionFoeVector_SetRange(swigCPtr, index, TraCIJunctionFoeVector.getCPtr(values));
    if (libtraciPINVOKE.SWIGPendingException.Pending) throw libtraciPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
