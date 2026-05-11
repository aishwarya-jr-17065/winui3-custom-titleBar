# WinUI3 Custom Title Bar – Expand/Collapse Button

A minimal WinUI 3 (Windows App SDK) sample that shows how to embed a custom **expand/collapse toggle button** in the title bar.

## What it does

* The title bar is fully custom (`ExtendsContentIntoTitleBar = true`).
* A single button sits just to the left of the system buttons (minimise / maximise / close).
* Clicking the button **changes its icon** between ChevronUp (⌃ — expanded) and ChevronDown (⌄ — collapsed) and toggles the page content area.

## Requirements

| Tool | Version |
|------|---------|
| Visual Studio 2022 | 17.x |
| Windows App SDK | 1.6 |
| Target OS | Windows 10 1809+ (build 17763) |

## Build & run

```
dotnet build WinUI3CustomTitleBar/WinUI3CustomTitleBar.csproj
```

Or open the `.csproj` in Visual Studio 2022 and press **F5**.