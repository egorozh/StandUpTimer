namespace StandUpTimer.Core.Models;

internal static class TimeExtensions
{
    public static Status GetNowStatus(this TimerSettings settings)
    {
        if (settings.Day == Day.None)
            return new AllDaysUnsettedStatus();

        var now = DateTime.Now;
        var nowDay = FromDayOfWeek(now.DayOfWeek);
        var nowTime = now.TimeOfDay;

        if (IsWorkDay(settings.Day, nowDay) && IsWorkTime(settings.FromTime, settings.ToTime, nowTime))
            return GetWorkStatus(settings.FromTime, settings.EveryPeriod, settings.StandTime, nowTime);

        return new TimerNotWorkingStatus();
    }

    public static Notify? GetClosestNotify(this TimerSettings settings, Status status) => status switch
    {
        AllDaysUnsettedStatus s => null,
        SittingPeriodStatus s => settings.GetNextNotify(s),
        StandUpPeriodStatus s => settings.GetNextNotify(s),
        TimerNotWorkingStatus s => null,
        _ => null
    };

    private static Notify GetNextNotify(this TimerSettings settings, SittingPeriodStatus status)
    {
        var sitTo = status.SitTo;

        if (sitTo < settings.ToTime)
            return new GoStandUpNotify(sitTo);
        else
            return new EndWorkDayNotify(settings.ToTime);
    }

    private static Notify GetNextNotify(this TimerSettings settings, StandUpPeriodStatus status)
    {
        var standTo = status.StandTo;

        if (standTo < settings.ToTime)
            return new GoSitNotify(standTo);
        else
            return new EndWorkDayNotify(settings.ToTime);
    }

    private static Status GetWorkStatus(in TimeSpan from, in TimeSpan every, in TimeSpan stand, in TimeSpan now)
    {
        var deltaNow = now - from;
        var phasePeriod = every + stand;
        var phaseCount = (int) (deltaNow / phasePeriod);

        var standFrom = from + every * (phaseCount + 1) + stand * phaseCount;
        var standTo = standFrom + stand;

        if (now >= standFrom && now <= standTo)
            return new StandUpPeriodStatus(standTo);

        var sitFrom = from + every * phaseCount + stand * phaseCount;
        var sitTo = sitFrom + every;

        return new SittingPeriodStatus(sitTo);
    }

    private static Day GetNextDay(Day settingsDay, Day nowDay)
    {
        var value = (int) settingsDay;

        //Todo: get next day

        return nowDay;
    }

    private static bool IsWorkTime(in TimeSpan from, in TimeSpan to, in TimeSpan now)
        => now >= from && now <= to;

    private static bool IsWorkDay(in Day settingsDay, in Day nowDay)
        => settingsDay.HasFlag(nowDay);

    private static Day FromDayOfWeek(in DayOfWeek day) => day switch
    {
        DayOfWeek.Sunday => Day.Sunday,
        DayOfWeek.Monday => Day.Monday,
        DayOfWeek.Tuesday => Day.Tuesday,
        DayOfWeek.Wednesday => Day.Wednesday,
        DayOfWeek.Thursday => Day.Thursday,
        DayOfWeek.Friday => Day.Friday,
        DayOfWeek.Saturday => Day.Saturday,
        _ => Day.None
    };
}