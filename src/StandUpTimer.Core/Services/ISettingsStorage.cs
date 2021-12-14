using StandUpTimer.Core.Models;

namespace StandUpTimer.Core.Services;

public interface ISettingsStorage
{
    TimerSettings GetSettings();

    void SetSettings(TimerSettings settings);
}