using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using StandUpTimer.Core.Services;

namespace StandUpTimer.UI.Services;

public class ShutdownService : IShutdownService
{
    public void Shutdown()
    {
        if (Application.Current is {ApplicationLifetime: IClassicDesktopStyleApplicationLifetime desktop})
            desktop.Shutdown();
    }
}