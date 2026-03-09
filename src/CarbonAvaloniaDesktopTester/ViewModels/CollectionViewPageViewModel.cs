using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarbonAvaloniaDesktopTester.ViewModels;

public class CollectionViewPageViewModel : ObservableObject
{
    public ObservableCollection<PersonItem> People { get; } =
    [
        new("Alice", "Smith", "Engineering"),
        new("Bob", "Johnson", "Marketing"),
        new("Charlie", "Williams", "Engineering"),
        new("Diana", "Brown", "Sales"),
        new("Eve", "Jones", "Marketing"),
        new("Frank", "Garcia", "Engineering"),
        new("Grace", "Miller", "Sales"),
        new("Hank", "Davis", "Marketing"),
        new("Ivy", "Rodriguez", "Engineering"),
        new("Jack", "Wilson", "Sales"),
    ];

    public string FilterText { get; set => SetProperty(ref field, value); } = string.Empty;

    public IRelayCommand AddPersonCommand { get; }
    public IRelayCommand RemoveLastCommand { get; }

    private int _addCounter;

    public CollectionViewPageViewModel()
    {
        AddPersonCommand = new RelayCommand(AddPerson);
        RemoveLastCommand = new RelayCommand(RemoveLast, () => People.Count > 0);
        People.CollectionChanged += (_, _) => RemoveLastCommand.NotifyCanExecuteChanged();
    }

    private void AddPerson()
    {
        _addCounter++;
        People.Add(new PersonItem($"New{_addCounter}", $"Person{_addCounter}", _addCounter % 2 == 0 ? "Engineering" : "Sales"));
    }

    private void RemoveLast()
    {
        if (People.Count > 0)
            People.RemoveAt(People.Count - 1);
    }
}

public record PersonItem(string FirstName, string LastName, string Department);
