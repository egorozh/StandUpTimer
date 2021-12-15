using System.Timers;

namespace StandUpTimer.Core.Models;

internal class StandTimer
{
    private Timer? _timer;
    private Notify? _closestNotify;

    public event Action<Status>? StatusChanged;
    public event Action<Notify>? Notify;

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

            _timer = new Timer(delta);
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();
        }
    }

    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        if (_closestNotify != null)
        {
            Notify?.Invoke(_closestNotify);
        }
    }

    private void DisposeTimer()
    {
        if (_timer != null)
        {
            _timer.Elapsed -= TimerOnElapsed;
            _timer.Stop();
            _timer.Dispose();
        }
    }
}