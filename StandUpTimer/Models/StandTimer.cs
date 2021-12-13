using System;

namespace StandUpTimer.Models;

internal class StandTimer
{
    public event Action<Notify>? NotifyChanged;

    public void Start(TimerSettings settings)
    {
        var closestNotify = GetClosestNotify(settings);

        NotifyChanged?.Invoke(closestNotify);
    }

    private Notify GetClosestNotify(TimerSettings settings)
    {
        var now = DateTime.Now;
        var nowDay = FromDayOfWeek(now.DayOfWeek);

        if (settings.Day == Day.None)
            return new AllDaysUnsettedNotify();

        if (IsWorkDay(settings.Day, nowDay))
        {
        }

        var nextDay = GetNextDay(settings.Day, nowDay);

        return new TimerNotWorkingNotify();
    }

    private Day GetNextDay(Day settingsDay, Day nowDay)
    {
        var value = (int) settingsDay;

        //Todo: get next day

        return nowDay;
    }

    private static bool IsWork(TimerSettings settings)
    {
        var now = DateTime.Now;
        var nowDay = FromDayOfWeek(now.DayOfWeek);

        if (!IsWorkDay(settings.Day, nowDay))
            return false;

        var nowTime = now.TimeOfDay;

        if (!IsWorkTime(settings.FromTime, settings.ToTime, nowTime))
            return false;

        return settings.StandTime.TotalMinutes > 0;
    }

    private static bool IsWorkTime(TimeSpan from, TimeSpan to, TimeSpan now)
        => now >= from && now <= to;

    private static bool IsWorkDay(Day settingsDay, Day nowDay)
        => settingsDay.HasFlag(nowDay);

    private static Day FromDayOfWeek(DayOfWeek day) => day switch
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