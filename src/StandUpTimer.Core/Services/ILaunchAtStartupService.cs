namespace StandUpTimer.Core.Services;

public interface ILaunchAtStartupService
{
    void AddOrRemoveApplicationToStartup(bool isAddToStartup);
}