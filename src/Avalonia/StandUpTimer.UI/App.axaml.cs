using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using StandUpTimer.UI.Services;
using System.Globalization;
using StandUpTimer.UI.ViewModels;

namespace StandUpTimer.UI;

public class App : Application
{
    private readonly IContainer _host = null!;

    public Action<ContainerBuilder> ServiceProvider
    {
        init
        {
            _host = Startup.GetHost(value);
            DataContext = _host.Resolve<ApplicationViewModel>();
        }
    }

    public App()
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-us");
        CultureInfo.CurrentUICulture = new CultureInfo("en-us");
    }

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            desktop.MainWindow = _host.Resolve<IWindowService>().CreateWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}