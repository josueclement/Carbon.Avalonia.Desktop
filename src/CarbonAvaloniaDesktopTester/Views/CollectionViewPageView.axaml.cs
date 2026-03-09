using System;
using System.ComponentModel;
using Avalonia.Controls;
using Carbon.Avalonia.Desktop.Data;
using CarbonAvaloniaDesktopTester.ViewModels;

namespace CarbonAvaloniaDesktopTester.Views;

public partial class CollectionViewPageView : UserControl
{
    public CollectionViewPageView()
    {
        InitializeComponent();

        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is CollectionViewPageViewModel vm)
        {
            vm.PropertyChanged += OnViewModelPropertyChanged;

            if (Resources.TryGetResource("SortedPeople", null, out var sorted) && sorted is CollectionViewSource sortedCvs)
                sortedCvs.Filter += OnFilter;

            if (Resources.TryGetResource("GroupedPeople", null, out var grouped) && grouped is CollectionViewSource groupedCvs)
                groupedCvs.Filter += OnFilter;
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(CollectionViewPageViewModel.FilterText))
            return;

        if (Resources.TryGetResource("SortedPeople", null, out var sorted) && sorted is CollectionViewSource sortedCvs)
            sortedCvs.View?.Refresh();

        if (Resources.TryGetResource("GroupedPeople", null, out var grouped) && grouped is CollectionViewSource groupedCvs)
            groupedCvs.View?.Refresh();
    }

    private void OnFilter(object? sender, FilterEventArgs e)
    {
        if (DataContext is not CollectionViewPageViewModel vm)
            return;

        var filter = vm.FilterText;
        if (string.IsNullOrWhiteSpace(filter))
        {
            e.Accepted = true;
            return;
        }

        if (e.Item is PersonItem person)
        {
            e.Accepted = person.FirstName.Contains(filter, StringComparison.OrdinalIgnoreCase)
                         || person.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase);
        }
    }
}
