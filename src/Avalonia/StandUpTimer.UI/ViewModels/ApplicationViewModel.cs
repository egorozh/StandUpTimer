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
    private void OpenMainWindow() => _windowService.ShowWindow();
}