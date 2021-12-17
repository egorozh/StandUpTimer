using StandUpTimer.Localization;

namespace StandUpTimer.Core.Models;

public abstract class Status
{
}

internal class SittingPeriodStatus : Status
{
    public TimeSpan SitTo { get; }

    public SittingPeriodStatus(TimeSpan sitTo)
    {
        SitTo = sitTo;
    }

    public override string ToString() => $"{LocalizationResources.Status_SittingPeriodStatus} {SitTo}";
}

internal class StandUpPeriodStatus : Status
{
    public TimeSpan StandTo { get; }

    public StandUpPeriodStatus(TimeSpan standTo)
    {
        StandTo = standTo;
    }

    public override string ToString() => $"{LocalizationResources.Status_StandUpPeriodStatus} {StandTo}";
}

internal class WorkingNextDayStatus : Status
{
    public DateTime NextStartTime { get; }

    public WorkingNextDayStatus(DateTime nextStartTime)
    {
        NextStartTime = nextStartTime;
    }
    
    public override string ToString() => $"{LocalizationResources.Status_TimerWillStart} {OfDay(NextStartTime.DayOfWeek)} " +
                                         $"({NextStartTime.ToShortDateString()}) {LocalizationResources.Status_At} " +
                                         $"{NextStartTime.ToShortTimeString()}";

    private static string OfDay(DayOfWeek day) => day switch
    {
        DayOfWeek.Sunday => LocalizationResources.Status_OnSunday,
        DayOfWeek.Monday => LocalizationResources.Status_OnMonday,
        DayOfWeek.Tuesday => LocalizationResources.Status_OnTuesday,
        DayOfWeek.Wednesday => LocalizationResources.Status_OnWednesday,
        DayOfWeek.Thursday => LocalizationResources.Status_OnThursday,
        DayOfWeek.Friday => LocalizationResources.Status_OnFriday,
        DayOfWeek.Saturday => LocalizationResources.Status_OnSaturday,
        _ => throw new ArgumentOutOfRangeException(nameof(day), day, null)
    };

}

internal class TimerNotWorkingStatus : Status
{
    public override string ToString() => LocalizationResources.Status_TimerNotWorkingStatus;
}

internal class AllDaysUnsettedStatus : Status
{
    public override string ToString() => LocalizationResources.Status_AllDaysUnsettedStatus;
}