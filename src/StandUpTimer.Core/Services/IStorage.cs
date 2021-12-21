namespace StandUpTimer.Core.Services;

public interface IStorage
{
    string UserDirectory { get; }
        
    string LogPath { get; }

    string SettingsFilePath { get; }
}