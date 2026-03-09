using System;
using Avalonia.Controls;
using Avalonia.Media;
using Carbon.Avalonia.Desktop.Controls.Navigation;
using Carbon.Avalonia.Desktop.Services;
using CarbonAvaloniaDesktopTester.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using PhosphorIconsAvalonia;

namespace CarbonAvaloniaDesktopTester.ViewModels;

public class MainWindowViewModel : ObservableObject
{
    private readonly IServiceProvider _services;

    public MainWindowViewModel(
        IServiceProvider services,
        INavigationService navigation,
        IContentDialogService dialogService,
        IOverlayService overlayService,
        IInfoBarService infoBarService)
    {
        _services = services;
        Navigation = navigation;
        DialogService = dialogService;
        OverlayService = overlayService;
        InfoBarService = infoBarService;

        Navigation.PageFactory = navItem =>
        {
            var page = _services.GetRequiredService(navItem.PageType);
            if (page is not Control ctrl)
                throw new InvalidOperationException($"Page type {navItem.PageType} is not a Control");
            ctrl.DataContext = _services.GetRequiredService(navItem.PageViewModelType);
            return ctrl;
        };
        Navigation.Items.Add(new NavigationItem
        {
            Header = "Base Controls",
            IconData = IconService.CreateGeometry(Icon.squares_four, IconType.regular),
            PageType = typeof(BaseControlsPageView),
            PageViewModelType = typeof(BaseControlsPageViewModel)
        });
        Navigation.Items.Add(new NavigationItem
        {
            Header = "Services with a very long header",
            IconData = IconService.CreateGeometry(Icon.chat_circle_text, IconType.regular),
            PageType = typeof(ServicesTestingPageView),
            PageViewModelType = typeof(ServicesTestingPageViewModel)
        });
        Navigation.Items.Add(new NavigationItem
        {
            Header = "Dialogs",
            IconData = IconService.CreateGeometry(Icon.files, IconType.regular),
            PageType = typeof(DialogsTestingPageView),
            PageViewModelType = typeof(DialogsTestingPageViewModel)
        });
        Navigation.Items.Add(new NavigationItem
        {
            Header = "Charts",
            IconData = IconService.CreateGeometry(Icon.chart_bar, IconType.regular),
            PageType = typeof(ChartsPageView),
            PageViewModelType = typeof(ChartsPageViewModel)
        });
        Navigation.Items.Add(new NavigationItem
        {
            Header = "Schedule",
            IconData = IconService.CreateGeometry(Icon.calendar, IconType.regular),
            PageType = typeof(SchedulePageView),
            PageViewModelType = typeof(SchedulePageViewModel)
        });
        Navigation.Items.Add(new NavigationItem
        {
            Header = "Ribbon",
            IconData = IconService.CreateGeometry(Icon.app_window, IconType.regular),
            PageType = typeof(RibbonTestingPageView),
            PageViewModelType = typeof(RibbonTestingPageViewModel)
        });
        Navigation.Items.Add(new NavigationItem
        {
            Header = "Docking",
            IconData = IconService.CreateGeometry(Icon.square_split_horizontal, IconType.regular),
            PageType = typeof(DockingTestingPageView),
            PageViewModelType = typeof(DockingTestingPageViewModel)
        });
        Navigation.Items.Add(new NavigationItem
        {
            Header = "Navigation",
            IconData = IconService.CreateGeometry(Icon.compass, IconType.regular),
            PageType = typeof(NavigationDemoPageView),
            PageViewModelType = typeof(NavigationDemoPageViewModel)
        });
        Navigation.Items.Add(new NavigationItem
        {
            Header = "Editors",
            IconData = Geometry.Parse("M3 17.25V21h3.75L17.81 9.94l-3.75-3.75L3 17.25zM20.71 7.04a1 1 0 0 0 0-1.41l-2.34-2.34a1 1 0 0 0-1.41 0l-1.83 1.83 3.75 3.75 1.83-1.83z"),
            PageType = typeof(EditorsTestingPageView),
            PageViewModelType = typeof(EditorsTestingPageViewModel)
        });
        Navigation.Items.Add(new NavigationItem
        {
            Header = "Displayer2D",
            IconData = IconService.CreateGeometry(Icon.pencil_ruler, IconType.regular),
            PageType = typeof(Displayer2DPageView),
            PageViewModelType = typeof(Displayer2DPageViewModel)
        });
        Navigation.Items.Add(new NavigationItem
        {
            Header = "Displayer2D Image",
            IconData = IconService.CreateGeometry(Icon.image, IconType.regular),
            PageType = typeof(Displayer2DImagePageView),
            PageViewModelType = typeof(Displayer2DImagePageViewModel)
        });
        Navigation.Items.Add(new NavigationItem
        {
            Header = "CollectionView",
            IconData = IconService.CreateGeometry(Icon.funnel, IconType.regular),
            PageType = typeof(CollectionViewPageView),
            PageViewModelType = typeof(CollectionViewPageViewModel)
        });

        Navigation.FooterItems.Add(new NavigationItem
        {
            Header = "Settings",
            IconData = IconService.CreateGeometry(Icon.gear, IconType.regular),
            PageType = typeof(SettingsPageView),
            PageViewModelType = typeof(SettingsPageViewModel)
        });

        Logo = new Avalonia.Controls.PathIcon
        {
            Data = Geometry.Parse("M12 2L2 7l10 5 10-5-10-5zM2 17l10 5 10-5M2 12l10 5 10-5"),
            Width = 28,
            Height = 28,
            Foreground = new SolidColorBrush(Color.FromRgb(99, 102, 241))
        };

        var servicesPage = _services.GetRequiredService<ServicesTestingPageView>();
        servicesPage.DataContext = _services.GetRequiredService<ServicesTestingPageViewModel>();
        Navigation.NavigateToAsync(servicesPage).GetAwaiter().GetResult();
    }

    public INavigationService Navigation { get; }
    public IContentDialogService DialogService { get; }
    public IOverlayService OverlayService { get; }
    public IInfoBarService InfoBarService { get; }
    public object Logo { get; }
}
