using System.Runtime.InteropServices;
using WinRT;

namespace DCompPrivateProjection.InteropCompositor
{
    public unsafe class InteropCompositorFactoryPartner : IDisposable
    {
        public static Guid IIDIInteropCompositorFactoryPartner = new Guid("22118adf-23f1-4801-bcfa-66cbf48cc51b");
        public static Guid IIDIInteropCompositorPartner = new Guid("e7894c70-af56-4f52-b382-4b3cd263dc6f");
        public static Guid IIDCompositor = new Guid("b403ca50-7f8c-4e83-985f-cc45060036d8");
        public static Guid IIDIDCompositionDesktopDevice = new Guid("5f4633fe-1e08-4cb8-8c75-ce24333f5602");

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DelegateCreateInteropCompositor(IntPtr ptrInstance, IntPtr renderingDevice, IntPtr callback, ref Guid iid, ref IntPtr instance);
        private IObjectReference _obj;

        public InteropCompositorFactoryPartner()
        {
            _obj = new WinRT.BaseActivationFactory("Windows.UI.Composition", "Windows.UI.Composition.Compositor")._As(IIDIInteropCompositorFactoryPartner);
        }

        public unsafe IntPtr CreateInteropCompositor(IntPtr renderingDevice, IntPtr callback, Guid iid)
        {
            // IInspectable
            var ptrCreateInteropCompositor = (*(IntPtr*)((*(IntPtr*) _obj.ThisPtr) + (nint) 6 * sizeof(void*)));
            var impCreateInteropCompositor = Marshal.GetDelegateForFunctionPointer<DelegateCreateInteropCompositor>(ptrCreateInteropCompositor);

            IntPtr instancePtr = IntPtr.Zero;
            ExceptionHelpers.ThrowExceptionForHR(impCreateInteropCompositor(_obj.ThisPtr, renderingDevice, callback, ref iid, ref instancePtr));
            if (instancePtr == IntPtr.Zero) throw new InvalidOperationException();
            return instancePtr;
        }

        public void Dispose()
        {
            _obj?.Dispose();
        }
    }
}
