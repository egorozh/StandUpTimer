using Serilog;
using StandUpTimer.Core.Services;
using System.Threading.Tasks;
using System.Timers;

namespace StandUpTimer.Core.Models;

public class StandTimer
{
    #region Private Fields

    private readonly ILogger _logger;
    private readonly INotifyService _notifyService;
    private readonly Timer _timer;

    private Notify? _closestNotify;
    private TimerSettings? _settings;

    #endregion

    #region Events

    public event Action<Status>? StatusChanged;

    #endregion

    #region Constructor

    public StandTimer(INotifyService notifyService, ILogger logger)
    {
        _logger = logger;
        _notifyService = notifyService;
        _timer = new Timer();
        _timer.Elapsed += TimerOnElapsed;
    }

    #endregion

    #region Public Methods

    public void Start(TimerSettings settings)
    {
        if (_timer.Enabled)
            _timer.Stop();

        _settings = settings;
        var status = settings.GetNowStatus();

        StatusChanged?.Invoke(status);

        _closestNotify = settings.GetClosestNotify(status);

        if (_closestNotify != null)
        {
            var now = DateTime.Now;

            double delta;

            if (_closestNotify is StartWorkDayNotify startWorkDay)
                delta = (startWorkDay.DateTime - now).TotalMilliseconds;
            else
                delta = (_closestNotify.Time - now.TimeOfDay).TotalMilliseconds;

            //var delta = 1000;
            _logger.Information($"StandTimer.Start: _closestNotify: {_closestNotify},{_closestNotify.Time}");
            _logger.Information($"StandTimer.Start: delta: {delta}, now: {now.ToLongTimeString()}");

            if (delta > 0)
                StartTimer(delta);
        }
    }

    #endregion

    #region Private Methods

    private void StartTimer(double delta)
    {
        _logger.Information($"StandTimer.StartTimer: delta: {delta}");

        try
        {
            _timer.Interval = delta;
            _timer.Start();
        }
        catch (Exception e)
        {
            _logger.Error(e, "StandTimer.StartTimer");
        }
    }

    private async void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        _logger.Information($"StandTimer.TimerOnElapsed: {e.SignalTime.ToLongTimeString()}");

        if (_closestNotify != null)
        {
            _logger.Information($"StandTimer.TimerOnElapsed: _closestNotify: {_closestNotify},{_closestNotify.Time}");

            try
            {
                await StandTimerOnNotify(_closestNotify);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "StandTimer.TimerOnElapsed");
            }
        }
    }

    private async Task StandTimerOnNotify(Notify notify)
    {
        await _notifyService.Notify(notify);

        await Task.Delay(1000);

        if (_settings != null)
            Start(_settings);
    }

    #endregion
}