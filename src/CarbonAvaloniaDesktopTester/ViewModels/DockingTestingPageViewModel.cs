using Avalonia.Controls;
using Avalonia.Layout;
using Carbon.Avalonia.Desktop.Controls.Docking;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarbonAvaloniaDesktopTester.ViewModels;

public class DockingTestingPageViewModel : ObservableObject
{
    public DockLayoutNode RootLayout { get; }

    public DockingTestingPageViewModel()
    {
        RootLayout = BuildLayoutModel();
    }

    private DockLayoutNode BuildLayoutModel()
    {
        // Create pane models
        var solutionPane = CreatePaneModel("Solution", "Solution Explorer content here", canClose: false, canMove: false);
        var propertiesPane = CreatePaneModel("Properties", "Properties panel content here", canClose: false);
        var outputPane = CreatePaneModel("Output", "Build output here");
        var debugPane = CreatePaneModel("Debug", "Debug console here");
        var doc1Pane = CreatePaneModel("Doc1", "Document 1 content");
        var doc2Pane = CreatePaneModel("Doc2", "Document 2 content");
        var doc3Pane = CreatePaneModel("Doc3", "Document 3 content");

        // Build model tree structure
        // Structure:
        // Root (Vertical Split)
        // ├─ Top (Horizontal Split)
        // │  ├─ Left (Horizontal Split)
        // │  │  ├─ Solution Group [Solution]
        // │  │  └─ Center Group [Doc1, Doc2, Doc3]
        // │  └─ Properties Group [Properties]
        // └─ Bottom Group [Output, Debug]

        var centerGroup = new DockTabGroupModel
        {
            SelectedPane = doc1Pane
        };
        centerGroup.Panes.Add(doc1Pane);
        centerGroup.Panes.Add(doc2Pane);
        centerGroup.Panes.Add(doc3Pane);

        var solutionGroup = new DockTabGroupModel
        {
            SelectedPane = solutionPane
        };
        solutionGroup.Panes.Add(solutionPane);

        var propertiesGroup = new DockTabGroupModel
        {
            SelectedPane = propertiesPane
        };
        propertiesGroup.Panes.Add(propertiesPane);

        var bottomGroup = new DockTabGroupModel
        {
            SelectedPane = outputPane
        };
        bottomGroup.Panes.Add(outputPane);
        bottomGroup.Panes.Add(debugPane);

        var leftCenterSplit = new DockSplitModel
        {
            Orientation = Orientation.Horizontal,
            First = solutionGroup,
            Second = centerGroup,
            FirstSize = new GridLength(200, GridUnitType.Pixel),
            SecondSize = new GridLength(1, GridUnitType.Star)
        };

        var topSplit = new DockSplitModel
        {
            Orientation = Orientation.Horizontal,
            First = leftCenterSplit,
            Second = propertiesGroup,
            FirstSize = new GridLength(1, GridUnitType.Star),
            SecondSize = new GridLength(200, GridUnitType.Pixel)
        };

        var rootSplit = new DockSplitModel
        {
            Orientation = Orientation.Vertical,
            First = topSplit,
            Second = bottomGroup,
            FirstSize = new GridLength(1, GridUnitType.Star),
            SecondSize = new GridLength(200, GridUnitType.Pixel)
        };

        return rootSplit;
    }

    private DockPaneModel CreatePaneModel(string header, string content, bool canClose = true, bool canMove = true)
    {
        // Create a simple view model for the pane content
        var contentViewModel = new PaneContentViewModel
        {
            Title = header,
            Description = content
        };

        return new DockPaneModel
        {
            Header = header,
            Content = contentViewModel,
            CanClose = canClose,
            CanMove = canMove
        };
    }
}

// Simple view model for pane content
public class PaneContentViewModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
