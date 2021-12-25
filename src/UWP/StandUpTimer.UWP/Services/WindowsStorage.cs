using StandUpTimer.Core.Services;
using System.IO;
using Windows.Storage;

namespace StandUpTimer.Services;

internal class WindowsStorage : IStorage
{
    public string UserDirectory { get; }
    public string LogPath { get; }
    public string SettingsFilePath { get; }

    public WindowsStorage()
    {
        UserDirectory = Path.Combine(ApplicationData.Current.LocalFolder.Path, "StandUpTimer");

        Directory.CreateDirectory(UserDirectory);
        
        SettingsFilePath = Path.Combine(UserDirectory, "settings.json");
        LogPath = Path.Combine(UserDirectory, "log-.txt");
    }
}