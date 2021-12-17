using StandUpTimer.Localization;

namespace StandUpTimer.Core.Models;

public abstract class Notify
{
    public virtual string Message => string.Empty;
    public virtual string Title => LocalizationResources.Notify_Reminder;
    public TimeSpan Time { get; protected set; }
}

public class GoStandUpNotify : Notify
{
    public override string Message => LocalizationResources.Notify_GoStandUpNotify;

    public GoStandUpNotify(TimeSpan time)
    {
        Time = time;
    }
}

public class GoSitNotify : Notify
{
    public override string Message => LocalizationResources.Notify_GoSitNotify;
    
    public GoSitNotify(TimeSpan time)
    {
        Time = time;
    }
}

public class EndWorkDayNotify : Notify
{
    public override string Message => LocalizationResources.Notify_EndWorkDayNotify;
   
    public EndWorkDayNotify(TimeSpan time)
    {
        Time = time;
    }
}

public class StartWorkDayNotify : Notify
{
    public override string Message => LocalizationResources.Notify_StartWorkDayNotify;
   
    public DateTime DateTime { get; }

    public StartWorkDayNotify(DateTime time)
    {
        DateTime = time;
    }
}