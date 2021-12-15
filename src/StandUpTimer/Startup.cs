using System.Runtime.InteropServices;
using Autofac;
using Serilog;
using StandUpTimer.Core.Services;
using StandUpTimer.Core.ViewModels;
using StandUpTimer.Services;

namespace StandUpTimer;

internal class Startup
{
    public static IContainer GetHost()
    {
        var builder = new ContainerBuilder();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        builder.RegisterInstance(Log.Logger).As<ILogger>().SingleInstance();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            builder.RegisterType<WindowsNotifyService>().As<INotifyService>().SingleInstance();
        }
        else
        {
            builder.RegisterType<DefaultNotifyService>().As<INotifyService>().SingleInstance();
        }
        
        builder.RegisterType<JsonSettingsSerializer>().As<ISettingsSerializer>().SingleInstance();
        builder.RegisterType<WindowsSettingsStorage>().As<ISettingsStorage>().SingleInstance();
        builder.RegisterType<MainWindowViewModel>().AsSelf();

        return builder.Build();
    }
}