using StandUpTimer.Core.Models;

namespace StandUpTimer.Core.Services;

public interface ISettingsSerializer
{
    TimerSettings Deserialize(string serializedStroke);
        
    string Serialize(TimerSettings settings);    
}