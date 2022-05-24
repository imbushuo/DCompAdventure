using Microsoft.Foundation;
using Windows.Foundation.Metadata;
using WinRT;

namespace Microsoft.UI.Composition.Private
{
    [WindowsRuntimeType("Microsoft.UI")]
    [System.Runtime.InteropServices.Guid("FE54DDBE-32CF-5EE0-84BC-3EF7FAEAD1C6")]
    [ContractVersion(typeof(WindowsAppSDKContract), 65536u)]
    public interface ISystemVisualProxyVisualPrivate
    {
    }

    [WindowsRuntimeType("Microsoft.UI")]
    [System.Runtime.InteropServices.Guid("B2CFCBC2-7133-4EF8-A686-DB7FD4D536B4")]
    [ContractVersion(typeof(WindowsAppSDKContract), 65536u)]
    internal interface ISystemVisualProxyVisualPrivateInterop
    {
        IntPtr GetHandle();
    }
}
