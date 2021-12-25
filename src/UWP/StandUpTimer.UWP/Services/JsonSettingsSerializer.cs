using Newtonsoft.Json;
using Serilog;
using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;
using System;

namespace StandUpTimer.Services;

public class JsonSettingsSerializer : ISettingsSerializer
{
    private readonly ILogger _logger;

    public JsonSettingsSerializer(ILogger logger)
    {
        _logger = logger;
    }

    public ApplicationSettings Deserialize(string serializedStroke)
    {
        try
        {
            return JsonConvert.DeserializeObject<ApplicationSettings>(serializedStroke) ?? new ApplicationSettings();
        }
        catch (Exception e)
        {
            _logger.Error(e, "JsonSettingsSerializer.Deserialize");

            return new ApplicationSettings();
        }
    }

    public string Serialize(ApplicationSettings settings)
    {
        try
        {
            return JsonConvert.SerializeObject(settings);
        }
        catch (Exception e)
        {
            _logger.Error(e, "JsonSettingsSerializer.Serialize");
            return string.Empty;
        }
    }
}