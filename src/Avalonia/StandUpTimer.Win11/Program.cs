using Avalonia;
using System;
using Autofac;
using StandUpTimer.Core.Services;
using StandUpTimer.UI;
using StandUpTimer.UI.Services;
using StandUpTimer.Win11.Services;

namespace StandUpTimer.Win11;

internal class Program
{
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure(CreateApp)
        .UsePlatformDetect()
        .With(new Win32PlatformOptions
        {
            UseWindowsUIComposition = true,
        })
        .LogToTrace();

    public static App CreateApp() => new()
    {
        ServiceProvider = builder =>
        {
            builder.RegisterType<WindowsNotifyService>().As<INotifyService>().SingleInstance();
            builder.RegisterType<WindowService>().As<IWindowService>().SingleInstance();
        }
    };
}