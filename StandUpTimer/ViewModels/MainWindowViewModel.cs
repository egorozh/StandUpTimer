using StandUpTimer.Models;

namespace StandUpTimer.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    public bool IsSunday { get; set; }
    public bool IsMonday { get; set; }
    public bool IsTuesday { get; set; }
    public bool IsWednesday { get; set; }
    public bool IsThursday { get; set; }
    public bool IsFriday { get; set; }
    public bool IsSaturday { get; set; }

    public TimerSettings TimerSettings { get; private set; }

    public MainWindowViewModel()
    {
        TimerSettings = new TimerSettings();

        SetActiveDays(TimerSettings.Day);
    }

    private void SetActiveDays(Day day)
    {
        IsSunday = day.HasFlag(Day.Sunday);
        IsMonday = day.HasFlag(Day.Monday);
        IsTuesday = day.HasFlag(Day.Tuesday);
        IsWednesday = day.HasFlag(Day.Wednesday);
        IsThursday = day.HasFlag(Day.Thursday);
        IsFriday = day.HasFlag(Day.Friday);
        IsSaturday = day.HasFlag(Day.Saturday);
    }
}