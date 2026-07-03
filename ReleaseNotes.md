# Release Notes

## Unreleased

### Navigation

- `NavigationView` — new `PaneSize` property (default 90) controls the rail's cross-axis extent (its width when vertical, its maximum height when horizontal), replacing the previously hard-coded `90`. Existing rails are unchanged.
- `NavigationItem` — new `LabelMaxWidth` property (default 72) controls the maximum label width in horizontal orientation, replacing the previously hard-coded `72`.

### Dialogs & Overlays

- `ContentDialog` — the dialog card is now sizable. New `StyledProperty` members `DialogWidth`, `DialogHeight`, `DialogMinWidth`, `DialogMaxWidth`, `DialogMinHeight`, and `DialogMaxHeight` are template-bound to the card, replacing the previously hard-coded `MinWidth="320"` / `MaxWidth="600"`. Width defaults are unchanged (320/600), so existing dialogs keep their current width; raise `DialogMaxWidth` for wide content. The content region is now wrapped in a `ScrollViewer`: content taller than the available height — or than an explicitly set `DialogMaxHeight` — scrolls within the card while the title and buttons stay fixed, instead of overflowing the window.

## 0.1.0 — 2026-02-26

Initial public release of **Carbon.Avalonia.Desktop**, a reusable Avalonia UI control library for .NET desktop applications.

---

### Navigation

- `NavigationView` — Shell-level navigation panel supporting horizontal and vertical orientations.
- `NavigationItem` — Individual navigation entry with icon, label, and parameter support.
- `NavigationService` — Programmatic navigation with lifecycle callbacks (`OnAppearingAsync`, `OnDisappearingAsync`), cancellable navigation, and serialized navigation via `SemaphoreSlim`.
- `INavigationViewModel` — Interface for ViewModels that participate in the navigation lifecycle.

### Docking

- `DockingHost` — Root host for a dockable layout.
- `DockPane` — Individual dockable content pane.
- `DockTabGroup` — Tab-based grouping of multiple panes.
- `DockSplitContainer` — Splitter container for arranging panes horizontally or vertically.
- `DockLayoutNode` / `DockPosition` — Layout tree model for serializable dock layouts.

### Ribbon

- `Ribbon` — Top-level ribbon control.
- `RibbonTab` — Tab within the ribbon.
- `RibbonGroup` — Logical group of commands within a tab.
- `RibbonButton` / `RibbonToggleButton` / `RibbonDropDownButton` — Command buttons with icon and label support.
- `RibbonMenuItem` — Menu item for ribbon drop-downs.

### Editors

A suite of strongly-typed inline editor controls sharing a common `BaseEditor<T>` foundation:

- **Text**: `TextEditor`, `MultiLineTextEditor`
- **Integer**: `IntEditor`, `ShortEditor`, `LongEditor`, `UIntEditor`, `UShortEditor`, `ULongEditor`
- **Floating-point**: `SingleEditor`, `DoubleEditor`, `DecimalEditor`
- **Binary / encoded**: `ByteArrayEditor`, `HexadecimalEditor`, `Base64Editor`

All editors support leading content, validation states, and consistent theming.

### Dialogs & Overlays

- `ContentDialog` + `IContentDialogService` — Modal dialog hosted as a sibling panel in the window root.
- `Overlay` + `IOverlayService` — Lightweight overlay panel for non-modal surfaces.
- `InfoBar` + `IInfoBarService` — Transient notification bar (informational, success, warning, error severities).

### Settings Controls

- `SettingsCard` — A labeled card for surfacing a single setting.
- `SettingsCardExpander` — An expandable variant for grouping related settings.

### File & Folder Dialogs

- `IFileDialogService` / `FileDialogService` — Async file picker with extension filter helpers.
- `IFolderDialogService` / `FolderDialogService` — Async folder picker.

### Data — CollectionView

A lightweight data pipeline for `IEnumerable` + `INotifyCollectionChanged` sources:

- `CollectionView` / `CollectionViewSource` — Observable view over a source collection.
- Filtering via `FilterEventArgs`.
- Sorting via `SortDescription` / `SortDirection`.
- Grouping via `PropertyGroupDescription` / `CollectionViewGroup`.

### Theme System

- Fluent theme integration (Dark and Light variants).
- `Colors.axaml` — Semantic color tokens via `ResourceDictionary.ThemeDictionaries`.
- `Brushes.axaml` — Named brushes (`CarbonBackground*`, `CarbonSurface*`, `CarbonBorder*`, `CarbonForeground*`, `CarbonAccent*`, `CarbonSuccess`, `CarbonWarning`, `CarbonError`) using `DynamicResource` for runtime theme switching.

---

### Early Access (Alpha)

The following controls are included in this release but are still in early development. APIs, templates, and behaviour are subject to breaking changes in future minor versions:

- **`CalendarSchedule`** — Calendar and event scheduling control. Basic rendering and item display are functional; interaction, recurrence, and customisation APIs are incomplete.
- **`Displayer2D`** — 2D canvas control with drawing objects (shapes, images, groups) and drag/user interaction support. Core rendering works; the public API surface and interaction model are still evolving.

---

### Requirements

| Dependency | Version |
|---|---|
| .NET | 10.0 |
| Avalonia | 11.3.12 |
| Avalonia.Themes.Fluent | 11.3.12 |
| CommunityToolkit.Mvvm | 8.4.0 |
| Microsoft.Extensions.DependencyInjection | 10.0.3 |
| PhosphorIconsAvalonia | 1.1.0 |
