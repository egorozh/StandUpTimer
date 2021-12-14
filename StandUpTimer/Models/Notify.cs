using System;

namespace StandUpTimer.Models;

internal abstract class Status
{
}
    
internal class SittingPeriodStatus : Status
{
    private readonly TimeSpan _sitTo;

    public SittingPeriodStatus(TimeSpan sitTo)
    {
        _sitTo = sitTo;
    }

    public override string ToString() => $"Сейчас время сидеть. Конец периода в {_sitTo}";
}

internal class StandUpPeriodStatus : Status
{
    private readonly TimeSpan _standTo;

    public StandUpPeriodStatus(TimeSpan standTo)
    {
        _standTo = standTo;
    }

    public override string ToString() => $"Сейчас время стоять. Конец периода в {_standTo}";
}

internal class TimerNotWorkingStatus : Status
{
    public override string ToString() => "Таймер выключен";
}

internal class AllDaysUnsettedStatus : Status
{
    public override string ToString() => "Не выбран ни один день работы таймера";
}