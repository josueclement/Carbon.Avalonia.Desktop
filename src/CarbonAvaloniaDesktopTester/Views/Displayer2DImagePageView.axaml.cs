using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using CarbonAvaloniaDesktopTester.ViewModels;

namespace CarbonAvaloniaDesktopTester.Views;

public partial class Displayer2DImagePageView : UserControl
{
    public Displayer2DImagePageView()
    {
        InitializeComponent();

        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, global::Avalonia.Interactivity.RoutedEventArgs e)
    {
        Displayer.ZoomToFit();

        Displayer.PropertyChanged += OnDisplayerPropertyChanged;
        RootGrid.PointerMoved += OnRootGridPointerMoved;
    }

    private void OnDisplayerPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == Carbon.Avalonia.Desktop.Controls.Displayer2D.Displayer2D.WorldMousePositionProperty
            && DataContext is Displayer2DImagePageViewModel vm)
        {
            vm.UpdateCoordinates((Point?)e.NewValue);
        }
    }

    private void OnRootGridPointerMoved(object? sender, PointerEventArgs e)
    {
        // Get the pointer position relative to the Displayer2D control
        var posInDisplayer = e.GetPosition(Displayer);

        // Convert canvas coordinates to world coordinates and update
        Displayer.WorldMousePosition = Displayer.CanvasToWorld(posInDisplayer);
    }
}
