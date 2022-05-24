using System.ComponentModel;
using System.Runtime.InteropServices;
using WinRT;
using WinRT.Interop;
using ImplNs = global::Microsoft.UI.Composition.Private;

namespace ABI.Microsoft.UI.Composition.Private
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct SystemVisualProxyVisualPrivate
    {
        public static IObjectReference CreateMarshaler(ImplNs.SystemVisualProxyVisualPrivate obj)
            => obj is null ? null : MarshalInspectable<ImplNs.SystemVisualProxyVisualPrivate>.CreateMarshaler(obj, true)
            .As<IUnknownVftbl>(GuidGenerator.GetIID(typeof(ImplNs.ISystemVisualProxyVisualPrivate).GetHelperType()));
        public static IntPtr GetAbi(IObjectReference value) => value != null ? MarshalInterfaceHelper<object>.GetAbi(value) : IntPtr.Zero;
        public static ImplNs.SystemVisualProxyVisualPrivate FromAbi(IntPtr thisPtr) => FromAbi(thisPtr);
        public static IntPtr FromManaged(ImplNs.SystemVisualProxyVisualPrivate obj) => (object)obj != null ? CreateMarshaler(obj).GetRef() : IntPtr.Zero;
        public static unsafe MarshalInterfaceHelper<ImplNs.SystemVisualProxyVisualPrivate>.MarshalerArray CreateMarshalerArray(ImplNs.SystemVisualProxyVisualPrivate[] array)
        {
            return MarshalInterfaceHelper<ImplNs.SystemVisualProxyVisualPrivate>.CreateMarshalerArray(array, o => CreateMarshaler(o));
        }
        public static (int length, IntPtr data) GetAbiArray(object box) => MarshalInterfaceHelper<ImplNs.SystemVisualProxyVisualPrivate>.GetAbiArray(box);
        public static unsafe ImplNs.SystemVisualProxyVisualPrivate[] FromAbiArray(object box) => MarshalInterfaceHelper<ImplNs.SystemVisualProxyVisualPrivate>.FromAbiArray(box, FromAbi);
        public static (int length, IntPtr data) FromManagedArray(ImplNs.SystemVisualProxyVisualPrivate[] array) 
            => MarshalInterfaceHelper<ImplNs.SystemVisualProxyVisualPrivate>.FromManagedArray(array, o => FromManaged(o));
        public static void DisposeMarshaler(IObjectReference value) => MarshalInspectable<object>.DisposeMarshaler(value);
        public static void DisposeMarshalerArray(
          MarshalInterfaceHelper<ImplNs.SystemVisualProxyVisualPrivate>.MarshalerArray array)
        {
            MarshalInterfaceHelper<ImplNs.SystemVisualProxyVisualPrivate>.DisposeMarshalerArray((object)array);
        }
        public static void DisposeAbi(IntPtr abi) => MarshalInspectable<object>.DisposeAbi(abi);
        public static void DisposeAbiArray(object box) => MarshalInspectable<object>.DisposeAbiArray(box);
    }
}
