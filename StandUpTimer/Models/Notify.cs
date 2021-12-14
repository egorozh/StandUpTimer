namespace StandUpTimer.Models;

internal abstract class Notify
{
}

internal class StartSittingPeriodNotify : Notify
{
}

internal class StartStandUpPeriodNotify : Notify
{
}

internal class TimerNotWorkingNotify : Notify
{
}

internal class AllDaysUnsettedNotify : Notify
{
}