﻿using System;
using System.IO;
using StandUpTimer.Core.Services;

namespace StandUpTimer.Services;

internal class WindowsStorage : IStorage
{
    public string UserDirectory { get; }
    public string LogPath { get; }
    public string SettingsFilePath { get; }

    public WindowsStorage()
    {
        UserDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "StandUpTimer");

        Directory.CreateDirectory(UserDirectory);
        
        SettingsFilePath = Path.Combine(UserDirectory, "settings.json");
        LogPath = Path.Combine(UserDirectory, "log-.txt");
    }
}