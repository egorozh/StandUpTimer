using Serilog;
using StandUpTimer.Core.Models;
using System.IO;

namespace StandUpTimer.Core.Services;

public class SettingsStorage : ISettingsStorage
{
    private readonly IStorage _storage;
    private readonly ISettingsSerializer _settingsSerializer;
    private readonly ILogger _logger;

    public SettingsStorage(IStorage storage, ISettingsSerializer settingsSerializer, ILogger logger)
    {
        _storage = storage;
        _settingsSerializer = settingsSerializer;
        _logger = logger;
    }

    public ApplicationSettings GetSettings()
    {
        try
        {
            return File.Exists(_storage.SettingsFilePath)
                ? _settingsSerializer.Deserialize(File.ReadAllText(_storage.SettingsFilePath))
                : new ApplicationSettings();
        }
        catch (Exception e)
        {
            _logger.Error(e, "SettingsStorage.GetSettings");
        }

        return new ApplicationSettings();
    }

    public void SetSettings(ApplicationSettings settings)
    {
        try
        {
            var json = _settingsSerializer.Serialize(settings);
            File.WriteAllText(_storage.SettingsFilePath, json);
        }
        catch (Exception e)
        {
            _logger.Error(e, "SettingsStorage.SetSettings");
        }
    }
}