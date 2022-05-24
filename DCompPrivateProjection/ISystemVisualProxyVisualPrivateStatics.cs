using Microsoft.Foundation;
using Windows.Foundation.Metadata;
using WinRT;

namespace Microsoft.UI.Composition.Private
{
    [WindowsRuntimeType("Microsoft.UI")]
    [System.Runtime.InteropServices.Guid("6efeef10-e0c5-5997-bcb7-c1644f1cab81")]
    [ContractVersion(typeof(WindowsAppSDKContract), 65536u)]
    internal interface ISystemVisualProxyVisualPrivateStatics
    {
        SystemVisualProxyVisualPrivate Create(Compositor compositor);
    }
}
