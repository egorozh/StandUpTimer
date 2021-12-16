using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;
using System.ComponentModel;
using System.Threading.Tasks;

namespace StandUpTimer.Core.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    #region Private Fields

    private readonly INotifyService _notifyService;
    private readonly ISettingsStorage _settingsStorage;
    private readonly ILaunchAtStartupService _startupService;
    private readonly StandTimer _standTimer;

    #endregion

    #region Public Properties

    public string? Message { get; set; }

    public bool IsSunday { get; set; }
    public bool IsMonday { get; set; }
    public bool IsTuesday { get; set; }
    public bool IsWednesday { get; set; }
    public bool IsThursday { get; set; }
    public bool IsFriday { get; set; }
    public bool IsSaturday { get; set; }

    public TimeSpan FromTime { get; set; }

    public TimeSpan ToTime { get; set; }

    /// <summary>
    /// min
    /// </summary>
    public int EveryPeriod { get; set; }

    /// <summary>
    /// min
    /// </summary>
    public int StandTime { get; set; }

    public bool LaunchAtStartup { get; set; }

    #endregion

    #region Constructor

    public MainWindowViewModel(INotifyService notifyService, ISettingsStorage settingsStorage,
        ILaunchAtStartupService startupService)
    {
        _notifyService = notifyService;
        _settingsStorage = settingsStorage;
        _startupService = startupService;

        var appSettings = _settingsStorage.GetSettings();

        var timerSettings = appSettings.TimerSettings;

        FromTime = timerSettings.FromTime;
        ToTime = timerSettings.ToTime;
        EveryPeriod = timerSettings.EveryPeriod.Minutes;
        StandTime = timerSettings.StandTime.Minutes;

        LaunchAtStartup = appSettings.LaunchAtStartup;

        SetActiveDays(timerSettings.Day);

        _standTimer = new StandTimer();

        _standTimer.StatusChanged += StandTimerOnStatusChanged;
        _standTimer.Notify += StandTimerOnNotify;
        _standTimer.Start(timerSettings);

        PropertyChanged += MainViewModelPropertyChanged;
    }

    #endregion

    #region Private Methods

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

    private void SetActiveDays(Day day)
    {
        IsMonday = day.HasFlag(Day.Monday);
        IsTuesday = day.HasFlag(Day.Tuesday);
        IsWednesday = day.HasFlag(Day.Wednesday);
        IsThursday = day.HasFlag(Day.Thursday);
        IsFriday = day.HasFlag(Day.Friday);
        IsSaturday = day.HasFlag(Day.Saturday);
        IsSunday = day.HasFlag(Day.Sunday);
    }

    private void MainViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
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

    private async void StandTimerOnNotify(Notify notify)
    {
        await _notifyService.Notify(notify);

        await Task.Delay(1000);

        var settings = GetSettings();

        _standTimer.Start(settings);
    }

    private void StandTimerOnStatusChanged(Status status)
    {
        Message = status.ToString();
    }

    #endregion
}