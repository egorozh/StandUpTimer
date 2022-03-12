using Avalonia.Controls;
using StandUpTimer.Core.ViewModels;
using StandUpTimer.UI;
using StandUpTimer.UI.Services;
using System;

namespace StandUpTimer.Services;

public class WindowService : IWindowService
{
    private readonly Func<MainWindowViewModel> _mainVmFactory;

    public WindowService(Func<MainWindowViewModel> mainVmFactory)
    {
        _mainVmFactory = mainVmFactory;
    }
    
    public Window CreateWindow() => new MainWindow
    {
        DataContext = _mainVmFactory.Invoke()
    };
}