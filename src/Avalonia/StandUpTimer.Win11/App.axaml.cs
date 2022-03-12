using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using StandUpTimer.Core.ViewModels;
using StandUpTimer.Win11.ViewModels;
using System.Globalization;

namespace StandUpTimer.Win11;

public class App : Application
{
    private readonly IContainer _host;

    public App()
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-us");
        CultureInfo.CurrentUICulture = new CultureInfo("en-us");
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