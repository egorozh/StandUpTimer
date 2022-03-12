using Autofac;
using Serilog;
using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;
using StandUpTimer.Core.ViewModels;
using StandUpTimer.UI.Services;
using StandUpTimer.UI.ViewModels;

namespace StandUpTimer.UI;

internal class Startup
{
    public static IContainer GetHost(Action<ContainerBuilder> serviceProvider)
    {
        var builder = new ContainerBuilder();

        serviceProvider.Invoke(builder);

        builder.Register(c =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(c.Resolve<IStorage>().LogPath,
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return Log.Logger;
        }).As<ILogger>().SingleInstance();
       
        builder.RegisterType<ShutdownService>().As<IShutdownService>().SingleInstance();
        builder.RegisterType<WindowsLaunchAtStartupService>().As<ILaunchAtStartupService>().SingleInstance();
        builder.RegisterType<JsonSettingsSerializer>().As<ISettingsSerializer>().SingleInstance();
        builder.RegisterType<WindowsStorage>().As<IStorage>().SingleInstance();
        builder.RegisterType<SettingsStorage>().As<ISettingsStorage>().SingleInstance();
        builder.RegisterType<StandTimer>().AsSelf().SingleInstance();
        builder.RegisterType<ApplicationViewModel>().AsSelf();
        builder.RegisterType<MainWindowViewModel>().AsSelf();

        return builder.Build();
    }
}