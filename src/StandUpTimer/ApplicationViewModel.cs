using System.ComponentModel;
using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Prism.Commands;
using StandUpTimer.Core.ViewModels;
using StandUpTimer.Views;
using System.Windows.Input;
using IContainer = Autofac.IContainer;

namespace StandUpTimer;

public class ApplicationViewModel : ViewModelBase
{
    private readonly IContainer _host;

    public ICommand ExitCommand { get; }
    public ICommand OpenMainWindowCommand { get; }

    public ApplicationViewModel(IContainer host)
    {
        _host = host;

        ExitCommand = new DelegateCommand(OnExit);
        OpenMainWindowCommand = new DelegateCommand(OpenMainWindow);
    }

    private void OnExit()
    {
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.Shutdown();
    }

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