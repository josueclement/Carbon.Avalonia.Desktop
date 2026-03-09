using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using Carbon.Avalonia.Desktop.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarbonAvaloniaDesktopTester.ViewModels;

public class DialogsTestingPageViewModel : ObservableObject
{
    private readonly IFileDialogService _fileDialogService;
    private readonly IFolderDialogService _folderDialogService;

    public DialogsTestingPageViewModel(
        IFileDialogService fileDialogService,
        IFolderDialogService folderDialogService)
    {
        _fileDialogService = fileDialogService;
        _folderDialogService = folderDialogService;

        // File dialog commands
        OpenSingleFileCommand = new AsyncRelayCommand(OpenSingleFile);
        OpenMultipleFilesCommand = new AsyncRelayCommand(OpenMultipleFiles);
        OpenTextFilesCommand = new AsyncRelayCommand(OpenTextFiles);
        SaveFileCommand = new AsyncRelayCommand(SaveFile);
        SaveFileWithExtensionCommand = new AsyncRelayCommand(SaveFileWithExtension);

        // Folder dialog commands
        OpenSingleFolderCommand = new AsyncRelayCommand(OpenSingleFolder);
        OpenMultipleFoldersCommand = new AsyncRelayCommand(OpenMultipleFolders);
    }

    public string? FileDialogResult
    {
        get;
        set => SetProperty(ref field, value);
    }

    public string? FolderDialogResult
    {
        get;
        set => SetProperty(ref field, value);
    }

    // File dialog commands
    public IAsyncRelayCommand OpenSingleFileCommand { get; }
    public IAsyncRelayCommand OpenMultipleFilesCommand { get; }
    public IAsyncRelayCommand OpenTextFilesCommand { get; }
    public IAsyncRelayCommand SaveFileCommand { get; }
    public IAsyncRelayCommand SaveFileWithExtensionCommand { get; }

    // Folder dialog commands
    public IAsyncRelayCommand OpenSingleFolderCommand { get; }
    public IAsyncRelayCommand OpenMultipleFoldersCommand { get; }

    // File dialog methods
    private async Task OpenSingleFile()
    {
        var files = await _fileDialogService.ShowOpenFileDialogAsync(
            title: "Select a file",
            allowMultiple: false);

        FileDialogResult = files.Any()
            ? $"Selected: {files.First()}"
            : "No file selected";
    }

    private async Task OpenMultipleFiles()
    {
        var files = await _fileDialogService.ShowOpenFileDialogAsync(
            title: "Select multiple files",
            allowMultiple: true);

        var fileList = files.ToList();
        FileDialogResult = fileList.Any()
            ? $"Selected {fileList.Count} file(s):\n{string.Join("\n", fileList)}"
            : "No files selected";
    }

    private async Task OpenTextFiles()
    {
        var textFileFilter = new FilePickerFileType("Text Files")
        {
            Patterns = new[] { "*.txt", "*.md" },
            MimeTypes = new[] { "text/plain", "text/markdown" }
        };

        var files = await _fileDialogService.ShowOpenFileDialogAsync(
            title: "Select text files",
            allowMultiple: true,
            fileTypeFilter: new[] { textFileFilter, FilePickerFileTypes.All });

        var fileList = files.ToList();
        FileDialogResult = fileList.Any()
            ? $"Selected {fileList.Count} text file(s):\n{string.Join("\n", fileList)}"
            : "No files selected";
    }

    private async Task SaveFile()
    {
        var file = await _fileDialogService.ShowSaveFileDialogAsync(
            title: "Save file",
            suggestedFileName: "document");

        FileDialogResult = file is not null
            ? $"Save location: {file}"
            : "Save cancelled";
    }

    private async Task SaveFileWithExtension()
    {
        var textFileType = new FilePickerFileType("Text File")
        {
            Patterns = new[] { "*.txt" }
        };

        var markdownFileType = new FilePickerFileType("Markdown File")
        {
            Patterns = new[] { "*.md" }
        };

        var file = await _fileDialogService.ShowSaveFileDialogAsync(
            title: "Save document",
            suggestedFileName: "document",
            defaultExtension: "txt",
            fileTypeChoices: new[] { textFileType, markdownFileType, FilePickerFileTypes.All });

        FileDialogResult = file is not null
            ? $"Save location: {file}"
            : "Save cancelled";
    }

    // Folder dialog methods
    private async Task OpenSingleFolder()
    {
        var folders = await _folderDialogService.ShowOpenFolderDialogAsync(
            title: "Select a folder",
            allowMultiple: false);

        var folderList = folders.ToList();
        FolderDialogResult = folderList.Any()
            ? $"Selected: {folderList.First()}"
            : "No folder selected";
    }

    private async Task OpenMultipleFolders()
    {
        var folders = await _folderDialogService.ShowOpenFolderDialogAsync(
            title: "Select multiple folders",
            allowMultiple: true);

        var folderList = folders.ToList();
        FolderDialogResult = folderList.Any()
            ? $"Selected {folderList.Count} folder(s):\n{string.Join("\n", folderList)}"
            : "No folders selected";
    }
}
