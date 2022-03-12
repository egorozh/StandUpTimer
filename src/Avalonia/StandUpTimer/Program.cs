using Autofac;
using Avalonia;
using StandUpTimer.Core.Services;
using StandUpTimer.Services;
using StandUpTimer.UI;
using System;
using StandUpTimer.UI.Services;

namespace StandUpTimer;

internal class Program
{
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
    
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure(CreateApp)
            .UsePlatformDetect()
            .LogToTrace();

    public static App CreateApp() => new()
    {
        ServiceProvider = builder =>
        {
            builder.RegisterType<DefaultNotifyService>().As<INotifyService>().SingleInstance();
            builder.RegisterType<WindowService>().As<IWindowService>().SingleInstance();
        }
    };
}