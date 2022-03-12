using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using StandUpTimer.Core.ViewModels;
using StandUpTimer.UI;
using StandUpTimer.UI.Services;
using System;

namespace StandUpTimer.Win11.Services;

public class WindowService : IWindowService
{
    private readonly Func<MainWindowViewModel> _mainVmFactory;

    public WindowService(Func<MainWindowViewModel> mainVmFactory)
    {
        _mainVmFactory = mainVmFactory;
    }

    public void ShowWindow()
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
            var window = CreateWindow();
            desktop.MainWindow = window;
            window.Show();
        }
    }

    public Window CreateWindow() => new MainWindow
    {
        DataContext = _mainVmFactory.Invoke(),
        TransparencyLevelHint = WindowTransparencyLevel.Mica,
        ExtendClientAreaToDecorationsHint = true
    };
}