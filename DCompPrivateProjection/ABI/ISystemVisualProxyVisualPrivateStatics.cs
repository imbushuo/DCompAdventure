using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WinRT;
using ImplNs = global::Microsoft.UI.Composition.Private;

namespace ABI.Microsoft.UI.Composition.Private
{
    [DynamicInterfaceCastableImplementation]
    [Guid("6efeef10-e0c5-5997-bcb7-c1644f1cab81")]
    internal unsafe interface ISystemVisualProxyVisualPrivateStatics : ImplNs.ISystemVisualProxyVisualPrivateStatics
    {
        static IntPtr AbiToProjectionVftablePtr;

        static unsafe ISystemVisualProxyVisualPrivateStatics()
        {
            AbiToProjectionVftablePtr = ComWrappersSupport.AllocateVtableMemory(typeof(ISystemVisualProxyVisualPrivateStatics), sizeof(IInspectable.Vftbl) + sizeof(IntPtr));
            *(IInspectable.Vftbl*)(void*) AbiToProjectionVftablePtr = IInspectable.Vftbl.AbiToProjectionVftable;
            ((delegate* unmanaged[Stdcall]<IntPtr, IntPtr, IntPtr*, int>*)AbiToProjectionVftablePtr)[6] = &Do_Abi_Create_0;
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
        private static unsafe int Do_Abi_Create_0(IntPtr thisPtr, IntPtr compositor, IntPtr* result)
        {
            *result = new IntPtr();
            try
            {
                var svPVPS = ComWrappersSupport.FindObject<ImplNs.ISystemVisualProxyVisualPrivateStatics>(thisPtr).Create(Compositor.FromAbi(compositor));
                *result = SystemVisualProxyVisualPrivate.FromManaged(svPVPS);
            }
            catch (Exception ex)
            {
                ExceptionHelpers.SetErrorInfo(ex);
                return ExceptionHelpers.GetHRForException(ex);
            }
            return 0;
        }

        unsafe ImplNs.SystemVisualProxyVisualPrivate ImplNs.ISystemVisualProxyVisualPrivateStatics.Create(global::Microsoft.UI.Composition.Compositor compositor)
        {
            var _obj = ((IWinRTObject)this).GetObjectReferenceForType(typeof(ImplNs.ISystemVisualProxyVisualPrivateStatics).TypeHandle);
            var ThisPtr = _obj.ThisPtr;
            IObjectReference iobjectReference = (IObjectReference)null;
            IntPtr num = new IntPtr();

            try
            {
                iobjectReference = Compositor.CreateMarshaler(compositor);
                ExceptionHelpers.ThrowExceptionForHR((*(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, IntPtr*, int>**)ThisPtr)[6](ThisPtr, Compositor.GetAbi(iobjectReference), (IntPtr*) &num));
                return ImplNs.SystemVisualProxyVisualPrivate.FromAbi(num);
            }
            finally
            {
                Compositor.DisposeMarshaler(iobjectReference);
                SystemVisualProxyVisualPrivate.DisposeAbi(num);
            }
        }
    }
}
