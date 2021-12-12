using StandUpTimer.Models;

namespace StandUpTimer.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    public TimerSettings TimerSettings { get; private set; }

    public MainWindowViewModel()
    {
        TimerSettings = new TimerSettings();
    }
}