using Microsoft.Win32;
using Serilog;
using StandUpTimer.Core.Services;
using System;

namespace StandUpTimer.Services;

internal class WindowsLaunchAtStartupService : ILaunchAtStartupService
{
    #region Private Fields

    private const string RegistryStartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
    private const string AppName = "StandUpTimer";

    private readonly ILogger _logger;
    private readonly string _appPath;

    #endregion

    #region Constructor

    public WindowsLaunchAtStartupService(ILogger logger)
    {
        _logger = logger;

        try
        {
            _appPath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ??
                       throw new Exception("Can't definition app path");
            _logger.Information($"AppPath: {_appPath}");
        }
        catch (Exception e)
        {
            _logger.Error(e, "LaunchAtStartupService");
            _appPath = string.Empty;
        }
    }

    #endregion

    #region Public Methods

    public void AddOrRemoveApplicationToStartup(bool isAddToStartup)
        => SetStartup(_logger, AppName, _appPath, isAddToStartup);

    #endregion

    #region Private Methods
    
    private static void SetStartup(ILogger logger, string appName, string appPath, bool enable)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            logger.Error(e, "SetStartup");
        }
    }

    #endregion
}