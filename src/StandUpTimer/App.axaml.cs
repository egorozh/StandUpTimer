using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using StandUpTimer.Core.ViewModels;
using StandUpTimer.Views;

namespace StandUpTimer;

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
            desktop.MainWindow = new MainWindow
            {
                DataContext = Startup.GetHost().Resolve<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}