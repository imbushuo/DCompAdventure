using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WinRT;
using ImplNs = global::Microsoft.UI.Composition.Private;

namespace ABI.Microsoft.UI.Composition.Private
{
    [DynamicInterfaceCastableImplementation]
    [Guid("B2CFCBC2-7133-4EF8-A686-DB7FD4D536B4")]
    internal unsafe interface ISystemVisualProxyVisualPrivateInterop : ImplNs.ISystemVisualProxyVisualPrivateInterop
    {
        /*
         *  .rdata:000000018016A228 ; const Microsoft::UI::Composition::Private::SystemVisualProxyVisualPrivate::Interop::`vftable'
            .rdata:000000018016A230   dq offset ?AddRef@?$NestedComImplements@VInteropCursorVisual@Composition@UI@Microsoft@@UIDCompositionCursorVisualPartner@@@WRL2@Microsoft@@UEAAKXZ ; 
                Microsoft::WRL2::NestedComImplements<Microsoft::UI::Composition::InteropCursorVisual,IDCompositionCursorVisualPartner>::AddRef(void)
            .rdata:000000018016A238   dq offset ?Release@?$NestedComImplements@VInteropCursorVisual@Composition@UI@Microsoft@@UIDCompositionCursorVisualPartner@@@WRL2@Microsoft@@UEAAKXZ ;
                Microsoft::WRL2::NestedComImplements<Microsoft::UI::Composition::InteropCursorVisual,IDCompositionCursorVisualPartner>::Release(void)
            .rdata:000000018016A240   dq offset ?GetHandle@Interop@SystemVisualProxyVisualPrivate@Private@Composition@UI@Microsoft@@UEAAJPEAPEAX@Z ;
         * This is IUnknown, not IInspectable. Handling it accordingly
         */
        static IntPtr AbiToProjectionVftablePtr = ComWrappersSupport.AllocateVtableMemory(typeof(ISystemVisualProxyVisualPrivate), sizeof(IntPtr) * 4);
        static unsafe ISystemVisualProxyVisualPrivateInterop()
        {
            *(IInspectable.Vftbl*)(void*)ISystemVisualProxyVisualPrivate.AbiToProjectionVftablePtr = IInspectable.Vftbl.AbiToProjectionVftable;
            ((delegate* unmanaged[Stdcall]<IntPtr, IntPtr*, int>*)AbiToProjectionVftablePtr)[3] = &Do_Abi_GetHandle_0;
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
        private static unsafe int Do_Abi_GetHandle_0(IntPtr thisPtr, IntPtr* result)
        {
            *result = new IntPtr();
            try
            {
                var handle = ComWrappersSupport.FindObject<ImplNs.ISystemVisualProxyVisualPrivateInterop>(thisPtr).GetHandle();
                *result = handle;
            }
            catch (Exception ex)
            {
                ExceptionHelpers.SetErrorInfo(ex);
                return ExceptionHelpers.GetHRForException(ex);
            }
            return 0;
        }

        unsafe IntPtr ImplNs.ISystemVisualProxyVisualPrivateInterop.GetHandle()
        {
            var _obj = ((IWinRTObject)this).GetObjectReferenceForType(typeof(ImplNs.ISystemVisualProxyVisualPrivateInterop).TypeHandle);
            var ThisPtr = _obj.ThisPtr;
            IntPtr num = new IntPtr();

            ExceptionHelpers.ThrowExceptionForHR((*(delegate* unmanaged[Stdcall]<IntPtr, IntPtr*, int>**)ThisPtr)[3](ThisPtr, (IntPtr*)&num));
            return num;
        }
    }
}
