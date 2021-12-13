using System;

namespace StandUpTimer.Models;

internal class StandTimer
{
    public void Start(TimerSettings settings)
    {
        var isWork = IsWork(settings);
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