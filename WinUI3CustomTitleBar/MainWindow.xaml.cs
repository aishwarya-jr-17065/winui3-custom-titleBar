using System;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinRT.Interop;

namespace WinUI3CustomTitleBar;

public sealed partial class MainWindow : Window
{
    private readonly AppWindow _appWindow;

    public MainWindow()
    {
        this.InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        _appWindow = GetAppWindowForCurrentWindow();
        ((FrameworkElement)Content).Loaded += MainWindow_Loaded;
        Activated += MainWindow_Activated;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        SetTitleBar(CustomDragRegion);

        if (AppWindowTitleBar.IsCustomizationSupported())
        {
            _appWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            _appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }
    }

    private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
    {
        MainTabView.Opacity = args.WindowActivationState == WindowActivationState.Deactivated ? 0.6 : 1.0;
    }

    private void PinToggleButton_Click(object sender, RoutedEventArgs e)
    {
        var isPinned = PinToggleButton.IsChecked == true;
        _appWindow.SetPresenter(isPinned ? AppWindowPresenterKind.CompactOverlay : AppWindowPresenterKind.Default);
    }

    private void MainTabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        sender.TabItems.Remove(args.Tab);

        if (sender.TabItems.Count == 0)
        {
            Close();
        }
    }

    private void MainTabView_AddTabButtonClick(TabView sender, object args)
    {
        sender.TabItems.Add(new TabViewItem
        {
            Header = $"New Tab {sender.TabItems.Count + 1}",
            IsClosable = true,
            Content = new TextBlock
            {
                Margin = new Thickness(12),
                Text = "New tab placeholder content"
            }
        });
    }

    private AppWindow GetAppWindowForCurrentWindow()
    {
        IntPtr hWnd = WindowNative.GetWindowHandle(this);
        WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
        return AppWindow.GetFromWindowId(windowId);
    }
}
