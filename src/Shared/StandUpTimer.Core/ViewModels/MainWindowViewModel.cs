using Serilog;
using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace StandUpTimer.Core.ViewModels;

public partial class MainWindowViewModel : ObservableObject, IDisposable
{
    #region Private Fields

    private readonly ISettingsStorage _settingsStorage;
    private readonly ILaunchAtStartupService _startupService;
    private readonly StandTimer _standTimer;
    private readonly ILogger _logger;

    #endregion

    #region Observable Fields

    [ObservableProperty] private string? _message;

    [ObservableProperty] private bool _isSunday;
    [ObservableProperty] private bool _isMonday;
    [ObservableProperty] private bool _isTuesday;
    [ObservableProperty] private bool _isWednesday;
    [ObservableProperty] private bool _isThursday;
    [ObservableProperty] private bool _isFriday;
    [ObservableProperty] private bool _isSaturday;

    [ObservableProperty] private TimeSpan _fromTime;

    [ObservableProperty] private TimeSpan _toTime;

    /// <summary>
    /// min
    /// </summary>
    [ObservableProperty] private int _everyPeriod;

    /// <summary>
    /// min
    /// </summary>
    [ObservableProperty] private int _standTime;

    [ObservableProperty] private bool _launchAtStartup;

    #endregion

    #region Constructor

    public MainWindowViewModel(ISettingsStorage settingsStorage,
        ILaunchAtStartupService startupService, StandTimer standTimer, ILogger logger)
    {
        _settingsStorage = settingsStorage;
        _startupService = startupService;
        _standTimer = standTimer;
        _logger = logger;

        var appSettings = _settingsStorage.GetSettings();

        var timerSettings = appSettings.TimerSettings;

        _fromTime = timerSettings.FromTime;
        _toTime = timerSettings.ToTime;
        _everyPeriod = timerSettings.EveryPeriod.Minutes;
        _standTime = timerSettings.StandTime.Minutes;

        _launchAtStartup = appSettings.LaunchAtStartup;

        void SetActiveDays(Day day)
        {
            _isMonday = day.HasFlag(Day.Monday);
            _isTuesday = day.HasFlag(Day.Tuesday);
            _isWednesday = day.HasFlag(Day.Wednesday);
            _isThursday = day.HasFlag(Day.Thursday);
            _isFriday = day.HasFlag(Day.Friday);
            _isSaturday = day.HasFlag(Day.Saturday);
            _isSunday = day.HasFlag(Day.Sunday);
        }

        SetActiveDays(timerSettings.Day);

        _standTimer.StatusChanged += StandTimerOnStatusChanged;
        _standTimer.Start(timerSettings);

        PropertyChanged += MainViewModelPropertyChanged;
    }

    #endregion

    #region Public Methods

    public void Dispose()
    {
        _standTimer.StatusChanged -= StandTimerOnStatusChanged;
        PropertyChanged -= MainViewModelPropertyChanged;
    }

    #endregion

    #region Private Methods

    private void MainViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        _logger.Information($"MainWindowViewModel.MainViewModelPropertyChanged(): propertyName: {e.PropertyName}");

        if (e.PropertyName == nameof(Message))
            return;

        var settings = GetSettings();

        _settingsStorage.SetSettings(new ApplicationSettings
        {
            LaunchAtStartup = LaunchAtStartup,
            TimerSettings = settings
        });

        if (e.PropertyName == nameof(LaunchAtStartup))
            _startupService.AddOrRemoveApplicationToStartup(LaunchAtStartup);
        else
            _standTimer.Start(settings);
    }

    private TimerSettings GetSettings() => new()
    {
        Day = GetActiveDays(),
        FromTime = FromTime,
        ToTime = ToTime,
        EveryPeriod = new TimeSpan(0, EveryPeriod, 0),
        StandTime = new TimeSpan(0, StandTime, 0),
    };

    private Day GetActiveDays()
    {
        var day = Day.None;

        if (IsMonday)
            day |= Day.Monday;
        if (IsTuesday)
            day |= Day.Tuesday;
        if (IsWednesday)
            day |= Day.Wednesday;
        if (IsThursday)
            day |= Day.Thursday;
        if (IsFriday)
            day |= Day.Friday;
        if (IsSaturday)
            day |= Day.Saturday;
        if (IsSunday)
            day |= Day.Sunday;

        return day;
    }

    private void StandTimerOnStatusChanged(Status status) => Message = status.ToString();

    #endregion
}