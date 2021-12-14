using Serilog;
using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;
using System;
using System.IO;

namespace StandUpTimer.Services;

internal class WindowsSettingsStorage : ISettingsStorage
{
    private readonly ISettingsSerializer _settingsSerializer;
    private readonly ILogger _logger;

    private const string SettingsFileName = "settings.json";

    public WindowsSettingsStorage(ISettingsSerializer settingsSerializer, ILogger logger)
    {
        _settingsSerializer = settingsSerializer;
        _logger = logger;
    }

    public TimerSettings GetSettings()
    {
        return File.Exists(SettingsFileName)
            ? _settingsSerializer.Deserialize(File.ReadAllText(SettingsFileName))
            : new TimerSettings();
    }

    public void SetSettings(TimerSettings settings)
    {
        try
        {
            var json = _settingsSerializer.Serialize(settings);
            File.WriteAllText(SettingsFileName, json);
        }
        catch (Exception e)
        {
            _logger.Error(e, "WindowsSettingsStorage.SetSettings");
        }
    }
}