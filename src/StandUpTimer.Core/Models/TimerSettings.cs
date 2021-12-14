namespace StandUpTimer.Core.Models;

public class TimerSettings
{
    public Day Day { get; set; } = Day.Monday | Day.Tuesday | Day.Wednesday | Day.Thursday | Day.Friday;
    
    public TimeSpan FromTime { get; set; } = new(10, 0, 0);
    
    public TimeSpan ToTime { get; set; } = new(18, 0, 0);
    
    public TimeSpan EveryPeriod { get; set; } = new(0, 45, 0);
    
    public TimeSpan StandTime { get; set; } = new(0, 15, 0);
}