using Autofac;
using Serilog;
using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;
using StandUpTimer.Core.ViewModels;
using StandUpTimer.UI.Services;
using StandUpTimer.Win11.Services;

namespace StandUpTimer.Win11;

internal class Startup
{
    public static IContainer GetHost()
    {
        var builder = new ContainerBuilder();

        builder.Register(c =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(c.Resolve<IStorage>().LogPath,
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return Log.Logger;
        }).As<ILogger>().SingleInstance();
        
        builder.RegisterType<WindowsNotifyService>().As<INotifyService>().SingleInstance();
        builder.RegisterType<WindowsLaunchAtStartupService>().As<ILaunchAtStartupService>().SingleInstance();
        builder.RegisterType<JsonSettingsSerializer>().As<ISettingsSerializer>().SingleInstance();
        builder.RegisterType<WindowsStorage>().As<IStorage>().SingleInstance();
        builder.RegisterType<SettingsStorage>().As<ISettingsStorage>().SingleInstance();
        builder.RegisterType<StandTimer>().AsSelf().SingleInstance();
        builder.RegisterType<MainWindowViewModel>().AsSelf();

        return builder.Build();
    }
}