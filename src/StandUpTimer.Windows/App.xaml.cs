﻿using Autofac;
using Microsoft.UI.Xaml;
using Serilog;
using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;
using StandUpTimer.Core.ViewModels;
using StandUpTimer.Services;

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

    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        var builder = new ContainerBuilder();

        Log.Logger = new LoggerConfiguration()
            //.WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        builder.RegisterInstance(Log.Logger).As<ILogger>().SingleInstance();

        builder.RegisterType<WindowsNotifyService>().As<INotifyService>().SingleInstance();
        builder.RegisterType<WindowsLaunchAtStartupService>().As<ILaunchAtStartupService>().SingleInstance();
        builder.RegisterType<JsonSettingsSerializer>().As<ISettingsSerializer>().SingleInstance();
        builder.RegisterType<WindowsSettingsStorage>().As<ISettingsStorage>().SingleInstance();
        builder.RegisterType<StandTimer>().AsSelf().SingleInstance();
        builder.RegisterType<MainWindowViewModel>().AsSelf();
        var host = builder.Build();

        var vm = host.Resolve<MainWindowViewModel>();

        m_window = new MainWindow(vm);

        m_window.Activate();
    }

    private Window m_window;
}