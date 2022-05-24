using System.Runtime.InteropServices;
using WinRT;
using ImplNs = global::Microsoft.UI.Composition.Private;

namespace ABI.Microsoft.UI.Composition.Private
{
    [DynamicInterfaceCastableImplementation]
    [Guid("FE54DDBE-32CF-5EE0-84BC-3EF7FAEAD1C6")]
    internal unsafe interface ISystemVisualProxyVisualPrivate : ImplNs.ISystemVisualProxyVisualPrivate
    {
        static IntPtr AbiToProjectionVftablePtr = ComWrappersSupport.AllocateVtableMemory(typeof(ISystemVisualProxyVisualPrivate), sizeof(IInspectable.Vftbl) + sizeof(IntPtr) * 2);
        static unsafe ISystemVisualProxyVisualPrivate()
        {
            *(IInspectable.Vftbl*)(void*) ISystemVisualProxyVisualPrivate.AbiToProjectionVftablePtr = IInspectable.Vftbl.AbiToProjectionVftable;
        }
    }
}
