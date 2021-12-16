using Serilog;
using StandUpTimer.Core.Models;
using System.Text.Json;

namespace StandUpTimer.Core.Services;

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
            return JsonSerializer.Deserialize<ApplicationSettings>(serializedStroke) ?? new ApplicationSettings();
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
            return JsonSerializer.Serialize(settings);
        }
        catch (Exception e)
        {
            _logger.Error(e, "JsonSettingsSerializer.Serialize");
            return string.Empty;
        }
    }
}