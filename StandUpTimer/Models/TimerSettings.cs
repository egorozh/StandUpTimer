using System;
using StandUpTimer.ViewModels;

namespace StandUpTimer.Models;

internal class TimerSettings : ViewModelBase
{
    public Day Day { get; set; } = Day.Monday | Day.Tuesday | Day.Wednesday | Day.Thursday | Day.Friday;

    public TimeOnly FromTime { get; set; } = new(10, 0);
    public TimeOnly ToTime { get; set; } = new(18, 0);

    public TimeOnly EveryPeriod { get; set; }
    public TimeOnly StandTime { get; set; }
}

[Flags]
internal enum Day
{
    Monday = 1,
    Tuesday = 2,
    Wednesday = 4,
    Thursday = 8,
    Friday = 16,
    Saturday = 32,
    Sunday = 64
}