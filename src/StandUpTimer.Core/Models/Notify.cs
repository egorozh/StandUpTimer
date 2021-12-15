namespace StandUpTimer.Core.Models;

public abstract class Notify
{
    public virtual string Message => string.Empty;
    public virtual string Title => string.Empty;
    public TimeSpan Time { get; protected set; }
}

internal class GoStandUpNotify : Notify
{
    public override string Message => "Пора вставать на ноги!";
    public override string Title => "Напоминание";

    public GoStandUpNotify(TimeSpan time)
    {
        Time = time;
    }
}

internal class GoSitNotify : Notify
{
    public override string Message => "Можно садиться!";
    public override string Title => "Напоминание";

    public GoSitNotify(TimeSpan time)
    {
        Time = time;
    }
}

internal class EndWorkDayNotify : Notify
{
    public override string Message => "Конец рабочего дня. Время расслабиться!)";
    public override string Title => "Напоминание";

    public EndWorkDayNotify(TimeSpan time)
    {
        Time = time;
    }
}

internal class StartWorkDayNotify : Notify
{
    public override string Message => "Начало рабочего дня!)";
    public override string Title => "Напоминание";
    
    public DateTime DateTime { get; }

    public StartWorkDayNotify(DateTime time)
    {
        DateTime = time;
    }
}