namespace StandUpTimer.Core.Models;

public class ApplicationSettings
{
    public bool LaunchAtStartup { get; set; }

    public TimerSettings TimerSettings { get; set; } = new();
}