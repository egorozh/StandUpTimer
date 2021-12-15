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

    public override string ToString() => $"Сейчас время сидеть. Конец периода в {SitTo}";
}

internal class StandUpPeriodStatus : Status
{
    public TimeSpan StandTo { get; }

    public StandUpPeriodStatus(TimeSpan standTo)
    {
        StandTo = standTo;
    }

    public override string ToString() => $"Сейчас время стоять. Конец периода в {StandTo}";
}

internal class WorkingNextDayStatus : Status
{
    public DateTime NextStartTime { get; }

    public WorkingNextDayStatus(DateTime nextStartTime)
    {
        NextStartTime = nextStartTime;
    }
    
    public override string ToString() => $"Таймер включится в {NextStartTime.DayOfWeek} " +
                                         $"({NextStartTime.ToShortDateString()}) в " +
                                         $"{NextStartTime.ToShortTimeString()}";
}

internal class TimerNotWorkingStatus : Status
{
    public override string ToString() => "Таймер выключен";
}

internal class AllDaysUnsettedStatus : Status
{
    public override string ToString() => "Не выбран ни один день работы таймера";
}