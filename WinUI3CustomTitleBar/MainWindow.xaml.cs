using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinRT.Interop;

namespace WinUI3CustomTitleBar;

public sealed partial class MainWindow : Window
{
    private readonly AppWindow _appWindow;
    private bool _isExpanded = true;

    public MainWindow()
    {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);

        _appWindow = GetAppWindow();
        if (AppWindowTitleBar.IsCustomizationSupported())
        {
            _appWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            _appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        // Update padding columns whenever the window state changes (e.g. maximised, snapped).
        _appWindow.Changed += AppWindow_Changed;

        // Set initial padding once the title bar element has been laid out.
        AppTitleBar.Loaded += (_, _) => UpdateTitleBarPadding();
    }

    private void AppWindow_Changed(AppWindow sender, AppWindowChangedEventArgs args)
    {
        if (args.DidPresenterChange)
            UpdateTitleBarPadding();
    }

    private void UpdateTitleBarPadding()
    {
        if (!AppWindowTitleBar.IsCustomizationSupported())
            return;

        LeftPaddingColumn.Width = new GridLength(_appWindow.TitleBar.LeftInset);
        RightPaddingColumn.Width = new GridLength(_appWindow.TitleBar.RightInset);
    }

    private void ExpandCollapseButton_Click(object sender, RoutedEventArgs e)
    {
        _isExpanded = !_isExpanded;

        // ChevronUp (U+E70E) = expanded state; ChevronDown (U+E70D) = collapsed state.
        BtnIcon.Glyph = _isExpanded ? "\uE70E" : "\uE70D";
        PageContent.Visibility = _isExpanded ? Visibility.Visible : Visibility.Collapsed;
    }

    private AppWindow GetAppWindow()
    {
        var handle = WindowNative.GetWindowHandle(this);
        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
        return AppWindow.GetFromWindowId(id);
    }
}
