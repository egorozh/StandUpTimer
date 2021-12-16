using System.Timers;
using Serilog;

namespace StandUpTimer.Core.Models;

public class StandTimer
{
    private readonly ILogger _logger;
    private Timer? _timer;
    private Notify? _closestNotify;

    public event Action<Status>? StatusChanged;
    public event Action<Notify>? Notify;

    public StandTimer(ILogger logger)
    {
        _logger = logger;
    }

    public void Start(TimerSettings settings)
    {
        DisposeTimer();

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

    private void StartTimer(double delta)
    {
        _logger.Information($"StandTimer.StartTimer: delta: {delta}");
        _timer = new Timer(delta);
        _timer.Elapsed += TimerOnElapsed;
        _timer.Start();
    }

    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        _logger.Information($"StandTimer.TimerOnElapsed: {e.SignalTime.ToLongTimeString()}");
        
        if (_closestNotify != null)
        {
            _logger.Information($"StandTimer.TimerOnElapsed: _closestNotify: {_closestNotify},{_closestNotify.Time}");
            Notify?.Invoke(_closestNotify);
        }
    }

    private void DisposeTimer()
    {
        _logger.Information("StandTimer.DisposeTimer() is calc");

        if (_timer != null)
        {
            _timer.Elapsed -= TimerOnElapsed;

            try
            {
                _timer.Dispose();
            }
            catch (Exception e)
            {
                _logger.Error(e, "StandTimer.DisposeTimer");
            }
        }
    }
}