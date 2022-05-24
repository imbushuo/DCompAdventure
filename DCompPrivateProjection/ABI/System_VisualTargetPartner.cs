using System.Runtime.InteropServices;
using WinRT;

namespace Windows.UI.Composition
{
    // Partial implementation for IVisualTargetPartner interface for system Composition, sufficient for what I need to do.
    [Guid("A1BEA8BA-D726-4663-8129-6B5E7927FFA6")]
    public unsafe class IVisualTargetPartner : IDisposable
    {
        public static Guid InterfaceId = new Guid("A1BEA8BA-D726-4663-8129-6B5E7927FFA6");
        public static Guid InterfaceIdStatics = new Guid("DBA1813C-60C5-4A42-A4D2-3380CDDCE8A1");
        private static Guid IIDUnknown = new Guid("00000000-0000-0000-C000-000000000046");

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DelegateGetRoot(IntPtr ptrInstance, ref IntPtr ptrRoot);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DelegateSetRoot(IntPtr ptrInstance, IntPtr ptrRoot);

        private IntPtr _vftableInternal;

        public IVisualTargetPartner(IntPtr ptr)
        {
            _vftableInternal = ptr;
        }

        public Visual GetRoot()
        {
            var ptrGetRoot = (*(IntPtr*)((*(IntPtr*)_vftableInternal) + (nint)3 * sizeof(void*)));
            var impGetRoot = Marshal.GetDelegateForFunctionPointer<DelegateGetRoot>(ptrGetRoot);

            IntPtr visualPtr = IntPtr.Zero;
            ExceptionHelpers.ThrowExceptionForHR(impGetRoot(_vftableInternal, ref visualPtr));
            if (visualPtr == IntPtr.Zero) throw new InvalidOperationException();
            return Visual.FromAbi(visualPtr);
        }

        public void SetRoot(Visual visual)
        {
            var ptrSetRoot = (*(IntPtr*)((*(IntPtr*)_vftableInternal) + (nint)4 * sizeof(void*)));
            var impSetRoot = Marshal.GetDelegateForFunctionPointer<DelegateSetRoot>(ptrSetRoot);

            (visual as ICustomQueryInterface).GetInterface(ref IIDUnknown, out IntPtr ptrIunknown);
            if (ptrIunknown == IntPtr.Zero) throw new InvalidCastException();
            ExceptionHelpers.ThrowExceptionForHR(impSetRoot(_vftableInternal, ptrIunknown));
            Marshal.Release(ptrIunknown);
        }

        public void Dispose()
        {
            if (_vftableInternal != IntPtr.Zero)
            {
                Marshal.Release(_vftableInternal);
                _vftableInternal = IntPtr.Zero;
            }
        }
    }
}
