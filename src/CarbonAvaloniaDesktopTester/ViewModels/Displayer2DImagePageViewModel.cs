using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Carbon.Avalonia.Desktop.Controls.Displayer2D;
using Carbon.Avalonia.Desktop.Controls.Displayer2D.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarbonAvaloniaDesktopTester.ViewModels;

public class Displayer2DImagePageViewModel : ObservableObject
{
    public IImage BackgroundImage { get; }
    public ObservableCollection<DrawingObject> Objects { get; }
    public UserInteraction Interaction { get; }

    public string CoordinateText { get; set => SetProperty(ref field, value); } = "X: —  Y: —";

    public Displayer2DImagePageViewModel()
    {
        BackgroundImage = CreateCheckerboardImage(640, 480, 32);

        Objects =
        [
            // Crosshair at image center (320, 240)
            new LineShape
            {
                X = 300, Y = 240, X2 = 340, Y2 = 240,
                Stroke = new SolidColorBrush(Color.Parse("#FF4444")),
                StrokeThickness = 2,
                ZIndex = 1
            },
            new LineShape
            {
                X = 320, Y = 220, X2 = 320, Y2 = 260,
                Stroke = new SolidColorBrush(Color.Parse("#FF4444")),
                StrokeThickness = 2,
                ZIndex = 1
            },
            new TextShape
            {
                X = 325, Y = 220,
                Text = "(320, 240)",
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.Parse("#FF4444")),
                ZIndex = 2
            },
            // Marker at top-left origin area (0, 0)
            new CircleShape
            {
                CenterX = 0, CenterY = 0, Width = 8, Height = 8,
                Fill = new SolidColorBrush(Color.Parse("#44FF44")),
                ZIndex = 1
            },
            new TextShape
            {
                X = 6, Y = 2,
                Text = "(0, 0)",
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.Parse("#44FF44")),
                ZIndex = 2
            },
            // Marker at bottom-right corner (640, 480)
            new CircleShape
            {
                CenterX = 640, CenterY = 480, Width = 8, Height = 8,
                Fill = new SolidColorBrush(Color.Parse("#FFAA00")),
                ZIndex = 1
            },
            new TextShape
            {
                X = 580, Y = 462,
                Text = "(640, 480)",
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.Parse("#FFAA00")),
                ZIndex = 2
            },
            // Rectangle overlay to demonstrate world-pixel alignment
            new RectangleShape
            {
                X = 128, Y = 96, Width = 128, Height = 96,
                Fill = new SolidColorBrush(Color.FromArgb(60, 53, 116, 240)),
                Stroke = new SolidColorBrush(Color.Parse("#3574F0")),
                StrokeThickness = 1.5,
                FillHover = new SolidColorBrush(Color.FromArgb(100, 53, 116, 240)),
                StrokeHover = new SolidColorBrush(Color.Parse("#FFFFFF")),
                ZIndex = 1
            }
        ];

        Interaction = new DragInteraction();
    }

    public void UpdateCoordinates(Point? worldPos)
    {
        if (worldPos is { } p)
            CoordinateText = $"X: {p.X:F2}  Y: {p.Y:F2}";
        else
            CoordinateText = "X: —  Y: —";
    }

    private static IImage CreateCheckerboardImage(int width, int height, int cellSize)
    {
        var darkBrush = new ImmutableSolidColorBrush(Color.FromRgb(40, 40, 40));
        var lightBrush = new ImmutableSolidColorBrush(Color.FromRgb(55, 55, 55));

        var group = new DrawingGroup();

        // Fill background with dark color
        group.Children.Add(new GeometryDrawing
        {
            Brush = darkBrush,
            Geometry = new RectangleGeometry(new Rect(0, 0, width, height))
        });

        // Draw light cells
        int cols = (width + cellSize - 1) / cellSize;
        int rows = (height + cellSize - 1) / cellSize;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if ((row + col) % 2 != 0) // light cell
                {
                    group.Children.Add(new GeometryDrawing
                    {
                        Brush = lightBrush,
                        Geometry = new RectangleGeometry(new Rect(col * cellSize, row * cellSize, cellSize, cellSize))
                    });
                }
            }
        }

        // Draw some grid lines for visual reference
        var gridPen = new Pen(new ImmutableSolidColorBrush(Color.FromArgb(40, 255, 255, 255)), 0.5);
        for (int x = 0; x <= width; x += cellSize * 4)
        {
            group.Children.Add(new GeometryDrawing
            {
                Pen = gridPen,
                Geometry = new LineGeometry(new Point(x, 0), new Point(x, height))
            });
        }
        for (int y = 0; y <= height; y += cellSize * 4)
        {
            group.Children.Add(new GeometryDrawing
            {
                Pen = gridPen,
                Geometry = new LineGeometry(new Point(0, y), new Point(width, y))
            });
        }

        return new DrawingImage(group);
    }
}
