using StandUpTimer.Core.Models;

namespace StandUpTimer.Core.Services;

public interface ISettingsStorage
{
    ApplicationSettings GetSettings();

    void SetSettings(ApplicationSettings settings);
}