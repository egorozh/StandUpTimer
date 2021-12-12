using System;
using StandUpTimer.ViewModels;

namespace StandUpTimer.Models;

internal class TimerSettings : ViewModelBase
{
    public bool IsSunday { get; set; }
    public bool IsMonday { get; set; } = true;
    public bool IsTuesday { get; set; } = true;
    public bool IsWednesday { get; set; } = true;
    public bool IsThursday { get; set; } = true;
    public bool IsFriday { get; set; } = true;
    public bool IsSaturday { get; set; }

    public TimeOnly FromTime { get; set; } = new(10, 0);
    public TimeOnly ToTime { get; set; } = new(18, 0);

    public TimeOnly EveryPeriod { get; set; }
    public TimeOnly StandTime { get; set; }
}