using Carbon.Avalonia.Desktop.Services;
using CarbonAvaloniaDesktopTester.ViewModels;
using CarbonAvaloniaDesktopTester.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CarbonAvaloniaDesktopTester;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public void AddHostedServices()
        {
            _ = services.AddHostedService<ApiHostedService>(); 
        }
        
        public void AddCarbonServices()
        {
            _ = services.AddSingleton<IFileDialogService, FileDialogService>();
            _ = services.AddSingleton<IFolderDialogService, FolderDialogService>();
            _ = services.AddSingleton<INavigationService, NavigationService>();
            _ = services.AddSingleton<IContentDialogService, ContentDialogService>();
            _ = services.AddSingleton<IInfoBarService, InfoBarService>();
            _ = services.AddSingleton<IOverlayService, OverlayService>();
        }
        
        public void AddPagesAndViewModels()
        {
            _ = services.AddSingleton<MainWindow>();
            // Views are Transient so each navigation creates a new instance
            // ViewModels remain Singleton to preserve state
            _ = services.AddTransient<BaseControlsPageView>();
            _ = services.AddTransient<ChartsPageView>();
            _ = services.AddTransient<DialogsTestingPageView>();
            _ = services.AddTransient<DockingTestingPageView>();
            _ = services.AddTransient<DummyPageView>();
            _ = services.AddTransient<EditorsTestingPageView>();
            _ = services.AddTransient<NavigationDemoPageView>();
            _ = services.AddTransient<RibbonTestingPageView>();
            _ = services.AddTransient<SchedulePageView>();
            _ = services.AddTransient<ServicesTestingPageView>();
            _ = services.AddTransient<Displayer2DPageView>();
            _ = services.AddTransient<Displayer2DImagePageView>();
            _ = services.AddTransient<SettingsPageView>();
            _ = services.AddTransient<CollectionViewPageView>();

            _ = services.AddSingleton<MainWindowViewModel>();
            _ = services.AddSingleton<BaseControlsPageViewModel>();
            _ = services.AddSingleton<ChartsPageViewModel>();
            _ = services.AddSingleton<DialogsTestingPageViewModel>();
            _ = services.AddSingleton<DockingTestingPageViewModel>();
            _ = services.AddSingleton<DummyPageViewModel>();
            _ = services.AddSingleton<EditorsTestingPageViewModel>();
            _ = services.AddSingleton<NavigationDemoPageViewModel>();
            _ = services.AddSingleton<RibbonTestingPageViewModel>();
            _ = services.AddSingleton<SchedulePageViewModel>();
            _ = services.AddSingleton<ServicesTestingPageViewModel>();
            _ = services.AddSingleton<Displayer2DPageViewModel>();
            _ = services.AddSingleton<Displayer2DImagePageViewModel>();
            _ = services.AddSingleton<SettingsPageViewModel>();
            _ = services.AddSingleton<CollectionViewPageViewModel>();
        }
    }
}
