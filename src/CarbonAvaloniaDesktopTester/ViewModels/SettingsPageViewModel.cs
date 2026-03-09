using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarbonAvaloniaDesktopTester.ViewModels;

public class SettingsPageViewModel : ObservableObject
{
    public SettingsPageViewModel()
    {
        CardClickedCommand = new RelayCommand<string?>(CardClicked);
        ToggleThemeCommand = new RelayCommand(ToggleTheme);
        ConfigureNotificationsCommand = new RelayCommand(() =>
        {
            LastAction = "Configuring notifications...";
        });

        // Initialize theme state
        UpdateThemeState();
    }

    public string? LastAction
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool IsDarkTheme
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                ApplyTheme(value);
            }
        }
    }

    public string? SelectedRegion
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool IsAutoSaveEnabled
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                LastAction = $"Auto-save {(value ? "enabled" : "disabled")}";
                OnPropertyChanged(nameof(AutoSaveButtonText));
            }
        }
    }

    public string AutoSaveButtonText => IsAutoSaveEnabled ? "Enabled" : "Disabled";

    public IRelayCommand CardClickedCommand { get; }
    public IRelayCommand ToggleThemeCommand { get; }
    public IRelayCommand ConfigureNotificationsCommand { get; }

    private void CardClicked(string? parameter)
    {
        LastAction = $"Clicked: {parameter}";
    }

    private void ToggleTheme()
    {
        IsDarkTheme = !IsDarkTheme;
    }

    private void ApplyTheme(bool isDark)
    {
        var app = Application.Current;
        if (app != null)
        {
            app.RequestedThemeVariant = isDark ? ThemeVariant.Dark : ThemeVariant.Light;
        }
    }

    private void UpdateThemeState()
    {
        var app = Application.Current;
        if (app != null)
        {
            IsDarkTheme = app.ActualThemeVariant == ThemeVariant.Dark;
        }
    }
}
