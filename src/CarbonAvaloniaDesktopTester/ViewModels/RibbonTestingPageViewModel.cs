using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarbonAvaloniaDesktopTester.ViewModels;

public class RibbonTestingPageViewModel : ObservableObject
{
    public int SelectedTabIndex
    {
        get;
        set => SetProperty(ref field, value);
    }

    public RibbonTestingPageViewModel()
    {
        NewCommand = new RelayCommand(New);
        OpenCommand = new RelayCommand(Open);
        SaveCommand = new RelayCommand(Save);
        CutCommand = new RelayCommand(Cut);
        CopyCommand = new RelayCommand(Copy);
        PasteCommand = new RelayCommand(Paste);
        UndoCommand = new RelayCommand(Undo);
        RedoCommand = new RelayCommand(Redo);
        ToggleBoldCommand = new RelayCommand(ToggleBold);
        ToggleItalicCommand = new RelayCommand(ToggleItalic);
        InsertImageCommand = new RelayCommand(InsertImage);
        InsertTableCommand = new RelayCommand(InsertTable);
        InsertLinkCommand = new RelayCommand(InsertLink);
        ExportPdfCommand = new RelayCommand(ExportPdf);
        ExportCsvCommand = new RelayCommand(ExportCsv);
        ExportJsonCommand = new RelayCommand(ExportJson);
    }

    public string StatusText
    {
        get;
        set => SetProperty(ref field, value);
    } = "Ready";

    public bool IsBoldActive
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool IsItalicActive
    {
        get;
        set => SetProperty(ref field, value);
    }

    public IRelayCommand NewCommand { get; }
    public IRelayCommand OpenCommand { get; }
    public IRelayCommand SaveCommand { get; }
    public IRelayCommand CutCommand { get; }
    public IRelayCommand CopyCommand { get; }
    public IRelayCommand PasteCommand { get; }
    public IRelayCommand UndoCommand { get; }
    public IRelayCommand RedoCommand { get; }
    public IRelayCommand ToggleBoldCommand { get; }
    public IRelayCommand ToggleItalicCommand { get; }
    public IRelayCommand InsertImageCommand { get; }
    public IRelayCommand InsertTableCommand { get; }
    public IRelayCommand InsertLinkCommand { get; }
    public IRelayCommand ExportPdfCommand { get; }
    public IRelayCommand ExportCsvCommand { get; }
    public IRelayCommand ExportJsonCommand { get; }

    private void New() => StatusText = "New file created";

    private void Open() => StatusText = "Open file dialog";

    private void Save() => StatusText = "File saved";

    private void Cut() => StatusText = "Cut to clipboard";

    private void Copy() => StatusText = "Copied to clipboard";

    private void Paste() => StatusText = "Pasted from clipboard";

    private void Undo() => StatusText = "Undo";

    private void Redo() => StatusText = "Redo";

    private void ToggleBold() => StatusText = IsBoldActive ? "Bold enabled" : "Bold disabled";

    private void ToggleItalic() => StatusText = IsItalicActive ? "Italic enabled" : "Italic disabled";

    private void InsertImage() => StatusText = "Insert image";

    private void InsertTable() => StatusText = "Insert table";

    private void InsertLink() => StatusText = "Insert link";

    private void ExportPdf() => StatusText = "Exported as PDF";

    private void ExportCsv() => StatusText = "Exported as CSV";

    private void ExportJson() => StatusText = "Exported as JSON";
}
