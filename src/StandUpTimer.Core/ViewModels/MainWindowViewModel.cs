using StandUpTimer.Core.Models;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using StandUpTimer.Core.Services;

namespace StandUpTimer.Core.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly INotifyService _notifyService;
    private readonly StandTimer _standTimer;

    private const string SettingsFileName = "settings.json";

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

    public TimeSpan EveryPeriod { get; set; }

    public TimeSpan StandTime { get; set; }

    public MainWindowViewModel(INotifyService notifyService)
    {
        _notifyService = notifyService;

        var timerSettings = File.Exists(SettingsFileName)
            ? FromJson(File.ReadAllText(SettingsFileName))
            : new TimerSettings();

        FromTime = timerSettings.FromTime;
        ToTime = timerSettings.ToTime;
        EveryPeriod = timerSettings.EveryPeriod;
        StandTime = timerSettings.StandTime;

        SetActiveDays(timerSettings.Day);

        _standTimer = new StandTimer();

        _standTimer.StatusChanged += StandTimerOnStatusChanged;
        _standTimer.Notify += StandTimerOnNotify;
        _standTimer.Start(timerSettings);

        PropertyChanged += MainViewModelPropertyChanged;
    }

    private async void StandTimerOnNotify(Notify notify)
    {
       await _notifyService.Notify(notify);

      

        var settings = GetSettings();
        
        _standTimer.Start(settings);
    }

    private void StandTimerOnStatusChanged(Status status)
    {
        Message = status.ToString();
    }

    private void MainViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var settings = GetSettings();

        var json = ToJson(settings);
        File.WriteAllText(SettingsFileName, json);
        _standTimer.Start(settings);
    }

    private TimerSettings GetSettings() => new()
    {
        Day = GetActiveDays(),
        FromTime = FromTime,
        ToTime = ToTime,
        EveryPeriod = EveryPeriod,
        StandTime = StandTime
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

    private TimerSettings FromJson(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<TimerSettings>(json) ?? new TimerSettings();
        }
        catch (Exception e)
        {
            return new TimerSettings();
        }
    }

    private string ToJson(TimerSettings settings)
    {
        try
        {
            return JsonSerializer.Serialize(settings);
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }
}