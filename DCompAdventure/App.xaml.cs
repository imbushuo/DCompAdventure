using DCompAdventure.Activation;
using DCompAdventure.Contracts.Services;
using DCompAdventure.Core.Contracts.Services;
using DCompAdventure.Core.Services;
using DCompAdventure.Helpers;
using DCompAdventure.Models;
using DCompAdventure.Services;
using DCompAdventure.Utilities;
using DCompAdventure.ViewModels;
using DCompAdventure.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;

using WinRT;

// To learn more about WinUI3, see: https://docs.microsoft.com/windows/apps/winui/winui3/.
namespace DCompAdventure
{
    public partial class App : Application
    {
        private Windows.System.DispatcherQueueController _controller;

        private ISystemBackdropController _backdropController;
        private SystemBackdropConfiguration _backdropPolicy;


        // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging
        private static IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Default Activation Handler
                services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

                // Other Activation Handlers

                // Services
                services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
                services.AddSingleton<ILocalSettingsService, LocalSettingsServicePackaged>();
                services.AddSingleton<IActivationService, ActivationService>();
                services.AddSingleton<IPageService, PageService>();
                services.AddSingleton<INavigationService, NavigationService>();

                // Core Services
                services.AddSingleton<IFileService, FileService>();

                // Views and ViewModels
                services.AddTransient<MainViewModel>();
                services.AddTransient<MainPage>();

                // Configuration
                services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
            })
            .Build();

        public static T GetService<T>()
            where T : class
            => _host.Services.GetService(typeof(T)) as T;

        public static Window MainWindow { get; set; } = new Window() { Title = "AppDisplayName".GetLocalized() };

        public App()
        {
            _controller = CoreMessagingHelper.CreateDispatcherQueueControllerForCurrentThread();

            InitializeComponent();
            UnhandledException += App_UnhandledException;
        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // TODO: Log and handle exceptions as appropriate.
            // For more details, see https://docs.microsoft.com/windows/winui/api/microsoft.ui.xaml.unhandledexceptioneventargs.
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            var activationService = App.GetService<IActivationService>();
            await activationService.ActivateAsync(args);
            DwmInterop.SetImmersiveDarkMode(WinRT.Interop.WindowNative.GetWindowHandle(MainWindow));
            TrySetWindowBackdrop();
        }

        private void TrySetWindowBackdrop()
        {
            if (MicaController.IsSupported())
            {
                var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(MainWindow);
                var acrylicController = new DesktopAcrylicController();
                _backdropPolicy = new SystemBackdropConfiguration();
                _backdropPolicy.IsInputActive = true;
                acrylicController.AddSystemBackdropTarget(App.MainWindow.As<ICompositionSupportsSystemBackdrop>());
                acrylicController.SetSystemBackdropConfiguration(_backdropPolicy);

                _backdropController = acrylicController;
            }
        }
    }
}
