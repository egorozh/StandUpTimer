using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StandUpTimer.Core.Services;
using StandUpTimer.UI.Services;

namespace StandUpTimer.UI.ViewModels;

public partial class ApplicationViewModel : ObservableObject
{
    private readonly IShutdownService _shutdownService;
    private readonly IWindowService _windowService;

    public ApplicationViewModel(IShutdownService shutdownService, IWindowService windowService)
    {
        _shutdownService = shutdownService;
        _windowService = windowService;
    }

    [ICommand]
    private void Exit() => _shutdownService.Shutdown();

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
            var window = _windowService.CreateWindow();
            desktop.MainWindow = window;
            window.Show();
        }
    }
}