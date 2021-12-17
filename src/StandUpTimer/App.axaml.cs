using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using StandUpTimer.Core.ViewModels;
using StandUpTimer.Views;

namespace StandUpTimer;

public class App : Application
{
    private readonly IContainer _host;

    public App()
    {
        _host = Startup.GetHost();
        DataContext = new ApplicationViewModel(_host);
    }

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            desktop.MainWindow = new MainWindow
            {
                DataContext = _host.Resolve<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}