using StandUpTimer.Core.Models;

namespace StandUpTimer.Core.Services;

public interface ISettingsSerializer
{
    ApplicationSettings Deserialize(string serializedStroke);
        
    string Serialize(ApplicationSettings settings);    
}