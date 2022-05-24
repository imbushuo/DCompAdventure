using System.Runtime.InteropServices;
using Windows.UI.Composition.CompositorCommon;
using Microsoft.UI.Composition.CompositorCommon;

namespace DCompPrivateProjection.ABI
{
    public static class CompositorExtension
    {
        public static Microsoft.UI.Composition.CompositorCommon.IPartner GetPartnerInstance(this Microsoft.UI.Composition.Compositor compositor)
        {
            var result = (compositor as ICustomQueryInterface).GetInterface(ref Microsoft.UI.Composition.CompositorCommon.IPartner.InterfaceId, out IntPtr instance);
            if (result != CustomQueryInterfaceResult.Handled || instance == IntPtr.Zero) throw new NotImplementedException();
            return new Microsoft.UI.Composition.CompositorCommon.IPartner(instance);
        }

        public static Windows.UI.Composition.CompositorCommon.IPartner GetPartnerInstance(this Windows.UI.Composition.Compositor compositor)
        {
            var result = (compositor as ICustomQueryInterface).GetInterface(ref Windows.UI.Composition.CompositorCommon.IPartner.InterfaceId, out IntPtr instance);
            if (result != CustomQueryInterfaceResult.Handled || instance == IntPtr.Zero) throw new NotImplementedException();
            return new Windows.UI.Composition.CompositorCommon.IPartner(instance);
        }
    }
}
