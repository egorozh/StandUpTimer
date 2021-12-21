using Autofac;
using Microsoft.UI.Xaml;
using StandUpTimer.Core.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace StandUpTimer;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }
    
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        var host = Startup.GetHost();

        var vm = host.Resolve<MainWindowViewModel>();

        m_window = new MainWindow(vm);

        m_window.Activate();
    }

    private Window m_window;
}