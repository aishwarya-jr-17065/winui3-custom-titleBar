using Microsoft.UI.Xaml;

namespace WinUI3CustomTitleBar;

public partial class App : Application
{
    public static Window Window { get; private set; } = null!;

    public App()
    {
        this.InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Window = new MainWindow();
        Window.Activate();
    }
}
