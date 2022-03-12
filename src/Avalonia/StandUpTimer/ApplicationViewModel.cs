using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StandUpTimer.Core.ViewModels;
using StandUpTimer.Views;
using System.Windows.Input;

namespace StandUpTimer;

public partial class ApplicationViewModel : ObservableObject
{
    private readonly IContainer _host;
    
    public ApplicationViewModel(IContainer host)
    {
        _host = host;
    }

    [ICommand]
    private void Exit()
    {
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.Shutdown();
    }

    [ICommand]
    private void OpenMainWindow()
    {
        if (Application.Current.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
            return;

        if (desktop.MainWindow is MainWindow {IsVisible: true} mainWindow)
        {
            mainWindow.WindowState = WindowState.Minimized;
            mainWindow.WindowState = WindowState.Normal;
            mainWindow.Activate();
        }
        else
        {
            var window = new MainWindow
            {
                DataContext = _host.Resolve<MainWindowViewModel>(),
            };
            desktop.MainWindow = window;
            window.Show();
        }
    }
}