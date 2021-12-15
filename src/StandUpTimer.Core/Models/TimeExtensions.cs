namespace StandUpTimer.Core.Models;

public static class TimeExtensions
{
    #region Public Methods

    public static Status GetNowStatus(this TimerSettings settings)
    {
        if (settings.Day == Day.None)
            return new AllDaysUnsettedStatus();

        if (settings.StandTime.Minutes <= 0 || settings.EveryPeriod.Minutes <= 0)
            return new TimerNotWorkingStatus();

        var now = DateTime.Now;
        var nowDay = FromDayOfWeek(now.DayOfWeek);
        var nowTime = now.TimeOfDay;

        if (IsWorkDay(settings.Day, nowDay) && IsWorkTime(settings.FromTime, settings.ToTime, nowTime))
            return GetWorkStatus(settings.FromTime, settings.EveryPeriod, settings.StandTime, nowTime);

        return new WorkingNextDayStatus(GetNextWorkStartDate(settings, now, nowDay));
    }

    public static Notify? GetClosestNotify(this TimerSettings settings, Status status) => status switch
    {
        AllDaysUnsettedStatus s => null,
        SittingPeriodStatus s => settings.GetNextNotify(s),
        StandUpPeriodStatus s => settings.GetNextNotify(s),
        TimerNotWorkingStatus s => null,
        WorkingNextDayStatus s => new StartWorkDayNotify(s.NextStartTime),
        _ => null
    };

    #endregion

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

    private static DayOfWeek FromDay(in Day day) => day switch
    {
        Day.Sunday => DayOfWeek.Sunday,
        Day.Monday => DayOfWeek.Monday,
        Day.Tuesday => DayOfWeek.Tuesday,
        Day.Wednesday => DayOfWeek.Wednesday,
        Day.Thursday => DayOfWeek.Thursday,
        Day.Friday => DayOfWeek.Friday,
        Day.Saturday => DayOfWeek.Saturday,
        _ => DayOfWeek.Friday
    };

    private static DateTime GetNextWorkStartDate(TimerSettings settings, DateTime now, Day nowDay)
    {
        var nextday = GetNextDay(settings.Day, nowDay);

        var date = GetDateNextDate(nextday, now);
        var time = settings.FromTime;

        return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
    }

    private static DateTime GetDateNextDate(Day nextday, DateTime now)
        => GetNextWeekday(now, FromDay(nextday));

    private static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
    {
        // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
        int daysToAdd = ((int) day - (int) start.DayOfWeek + 7) % 7;
        return start.AddDays(daysToAdd);
    }

    public static Day GetNextDay(Day settingsDays, Day nowDay)
    {
        var days = (byte) settingsDays;
        var nowDayV = (byte) nowDay;

        var nowIndex = 0;

        for (var i = 0; i < 8; i++)
        {
            if (GetBit(nowDayV, i))
            {
                nowIndex = i;
                break;
            }
        }

        var nextDayIndex = nowIndex;
        var flag = false;

        for (int i = nowIndex + 1; i < 8; i++)
        {
            if (GetBit(days, i))
            {
                nextDayIndex = i;
                flag = true;
                break;
            }
        }

        if (!flag)
        {
            for (byte i = 0; i < nowIndex; i++)
            {
                if (GetBit(days, i))
                {
                    nextDayIndex = i;
                    break;
                }
            }
        }

        return (Day) Math.Pow(2, nextDayIndex);

        static bool GetBit(byte b, int bitNumber) => (b & (1 << bitNumber)) != 0;
    }
}