using System;
using System.IO;
using Windows.Storage;
using Serilog;
using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;

namespace StandUpTimer.Services;

internal class WindowsSettingsStorage : ISettingsStorage
{
    private readonly ISettingsSerializer _settingsSerializer;
    private readonly ILogger _logger;
    private readonly string _settingsPath;

    private const string SettingsFileName = "settings.json";

    public WindowsSettingsStorage(ISettingsSerializer settingsSerializer, ILogger logger)
    {
        _settingsSerializer = settingsSerializer;
        _logger = logger;

        var folder = ApplicationData.Current.LocalFolder;
        _settingsPath = Path.Combine(folder.Path, SettingsFileName);
    }

    public TimerSettings GetSettings()
    {
       
        return File.Exists(_settingsPath)
            ? _settingsSerializer.Deserialize(File.ReadAllText(_settingsPath))
            : new TimerSettings();
    }

    public void SetSettings(TimerSettings settings)
    {
        try
        {
            var json = _settingsSerializer.Serialize(settings);
            File.WriteAllText(_settingsPath, json);
        }
        catch (Exception e)
        {
            _logger.Error(e, "WindowsSettingsStorage.SetSettings");
        }
    }
}