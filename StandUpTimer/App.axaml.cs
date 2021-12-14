using System.Runtime.InteropServices;
using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PropertyChanged;
using StandUpTimer.Core.Services;
using StandUpTimer.Core.ViewModels;
using StandUpTimer.Services;
using StandUpTimer.Views;

namespace StandUpTimer;

[DoNotNotify]
public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var builder = new ContainerBuilder();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                builder.RegisterType<WindowsNotifyService>().As<INotifyService>().SingleInstance();
            }
            else
            {
                builder.RegisterType<DefaultNotifyService>().As<INotifyService>().SingleInstance();
            }

            builder.RegisterType<MainWindowViewModel>().AsSelf();
            var host = builder.Build();

            desktop.MainWindow = new MainWindow
            {
                DataContext = host.Resolve<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}