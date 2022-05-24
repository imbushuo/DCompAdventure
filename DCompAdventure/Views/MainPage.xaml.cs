using System;
using System.Runtime.InteropServices;
using System.Threading;

using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using CommunityToolkit.Mvvm.Messaging;

using WinFormsComponent;
using WinFormsComponent.Messages;
using DCompAdventure.ViewModels;

using DCompPrivateProjection.ABI;
using DCompPrivateProjection.InteropCompositor;
using PlatformComposition = Windows.UI.Composition;
using UndockedComposition = Microsoft.UI.Composition;

namespace DCompAdventure.Views
{
    public sealed partial class MainPage : Page
    {
        private static Guid IIDPlatformIVisual = new Guid("117E202D-A859-4C89-873B-C2AA566788E3");

        private static Guid IIDPlatformVisualTargetPartnerStatics = new Guid("DBA1813C-60C5-4A42-A4D2-3380CDDCE8A1");
        private static Guid IIDPlatformVisualTargetPartner = new Guid("A1BEA8BA-D726-4663-8129-6B5E7927FFA6");

        public MainViewModel ViewModel { get; }

        private UndockedComposition.Compositor _compositor;
        private UndockedComposition.CompositorCommon.IPartner _compositorPartner;
        private UndockedComposition.Private.SystemVisualProxyVisualPrivate _svProxyInstance;
        private UndockedComposition.Visual _proxyVisual;
       
        private PlatformComposition.Compositor _systemCompositor;
        private PlatformComposition.CompositorCommon.IPartner _systemCompositorPartner;
        private PlatformComposition.IVisualTargetPartner _targetFromUndockedComposition;
        private InteropCompositorFactoryPartner _systemCompositorFactoryPartner;

        private SharpDX.Direct3D11.Device _d3d11Device;
        private SharpDX.DXGI.Device _dxgiDevice;
        private SharpDX.Direct2D1.Device _d2d1Device;

        private SharpDX.DirectComposition.DesktopDevice _dcompDesktopDevice;
        private SharpDX.IUnknown _hwndSurface;
        private SharpDX.DirectComposition.Visual _hwndVisual;
        private PlatformComposition.Visual hwndPlatformVisual;

        private Thread _axHostWindowThread;

        public MainPage()
        {
            ViewModel = App.GetService<MainViewModel>();
            InitializeComponent();

            this.Unloaded += MainPage_Unloaded;
            StrongReferenceMessenger.Default.Register<MainPage, WindowReadyMessage>(this, (r, m) => DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () => OnWinFormsReportedReady(r, m)));
        }

        private void OnLaunchWinFormsButtonClicked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var visual = ElementCompositionPreview.GetElementVisual(HwndHostingPresentationPanel);

            if (_svProxyInstance == null)
            {
                _compositor = visual.Compositor;
                _compositorPartner = _compositor.GetPartnerInstance();
                _svProxyInstance = UndockedComposition.Private.SystemVisualProxyVisualPrivate.Create(_compositor);
                _proxyVisual = _svProxyInstance.AsVisual();
            }

            if (_d3d11Device == null)
            {
                _d3d11Device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Hardware, SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport);
                _dxgiDevice = _d3d11Device.QueryInterface<SharpDX.DXGI.Device>();
                _d2d1Device = new SharpDX.Direct2D1.Device(_dxgiDevice);
            }

            if (_systemCompositor == null)
            {
                _systemCompositorFactoryPartner = new InteropCompositorFactoryPartner();
                var interopCompositor = _systemCompositorFactoryPartner.CreateInteropCompositor(_d2d1Device.NativePointer, IntPtr.Zero, InteropCompositorFactoryPartner.IIDIInteropCompositorPartner);

                // QI this to whatever thing (Platform + DComp)
                Marshal.QueryInterface(interopCompositor, ref InteropCompositorFactoryPartner.IIDCompositor, out IntPtr ptrPlatformCompositor);
                if (ptrPlatformCompositor == IntPtr.Zero) throw new NotImplementedException();
                _systemCompositor = PlatformComposition.Compositor.FromAbi(ptrPlatformCompositor);
                _systemCompositorPartner = _systemCompositor.GetPartnerInstance();
                _targetFromUndockedComposition = _systemCompositorPartner.OpenShardTargetFromHandle(_svProxyInstance.GetHandle());

                Marshal.QueryInterface(interopCompositor, ref InteropCompositorFactoryPartner.IIDIDCompositionDesktopDevice, out IntPtr ptrDCompDevice);
                if (ptrDCompDevice == IntPtr.Zero) throw new NotImplementedException();
                _dcompDesktopDevice = new SharpDX.DirectComposition.DesktopDevice(ptrDCompDevice);

                // QI complete, release this
                Marshal.Release(interopCompositor);
            }

            // Start WinForms, it will call us later
            _axHostWindowThread = new Thread(() => {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                System.Windows.Forms.Application.SetHighDpiMode(System.Windows.Forms.HighDpiMode.PerMonitorV2);
                System.Windows.Forms.Application.Run(new SimpleHwnd());
            });
            _axHostWindowThread.SetApartmentState(ApartmentState.STA);
            _axHostWindowThread.Start();
        }

        private async void OnWinFormsReportedReady(MainPage recipient, WindowReadyMessage message)
        {
            _hwndSurface = _dcompDesktopDevice.CreateSurfaceFromHwnd(message.WindowHandle);
            _hwndVisual = new SharpDX.DirectComposition.Visual2(_dcompDesktopDevice);

            _hwndVisual.Content = _hwndSurface;
            _hwndVisual.SetOffsetX(0);
            _hwndVisual.SetOffsetY(0);

            Marshal.QueryInterface(_hwndVisual.NativePointer, ref IIDPlatformIVisual, out IntPtr ptrHwndPlatformVisual);
            if (ptrHwndPlatformVisual == IntPtr.Zero) throw new NotImplementedException();
            hwndPlatformVisual = PlatformComposition.Visual.FromAbi(ptrHwndPlatformVisual);
            Marshal.Release(ptrHwndPlatformVisual);

            // Send this visual to current visual tree
            ElementCompositionPreview.SetElementChildVisual(HwndHostingPresentationPanel, _proxyVisual);
            // XXX: hardcoded size
            _proxyVisual.Size = new System.Numerics.Vector2(2373, 1303);
            _proxyVisual.Scale = new System.Numerics.Vector3(1.0f / message.DpiScaling, 1.0f / message.DpiScaling, 1.0f);
            _targetFromUndockedComposition.SetRoot(hwndPlatformVisual);

            // Note: DComp and Platform composition should only need one commit as they are actually the same thing
            // _dcompDesktopDevice.Commit();
            await _systemCompositor.RequestCommitAsync();
            await _compositor.RequestCommitAsync();
        }

        private void MainPage_Unloaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            _hwndVisual?.Dispose();
            hwndPlatformVisual?.Dispose();
            _hwndSurface?.Dispose();

            _dcompDesktopDevice?.Dispose();

            _targetFromUndockedComposition?.Dispose();
            _systemCompositorPartner?.Dispose();
            _systemCompositor?.Dispose();

            _compositorPartner?.Dispose();
            _compositor?.Dispose();

            _proxyVisual?.Dispose();
            _svProxyInstance?.Dispose();

            _d3d11Device?.Dispose();
            _dxgiDevice?.Dispose();

            _systemCompositorFactoryPartner?.Dispose();
        }
    }
}
