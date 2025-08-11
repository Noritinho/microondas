using System.Diagnostics;
using Microwave.Domain.Exceptions;

namespace Microwave.Domain.Models.Heating;

public class HeatingTimer
{
    public HeatingTimer(int? heatTime, DateTimeOffset startHeatDateTime)
    {
        Duration = CustomHeatingDuration.Create(heatTime);
        StartedDateTime = new HeatingStartDateTime(startHeatDateTime);
    }

    private HeatingTimer() { }
    
    private Timer _timer;
    private readonly Stopwatch _stopwatch = new();
    
    public HeatingDuration Duration { get; private set; }
    public HeatingStartDateTime StartedDateTime { get; private set; }

    public static HeatingTimer FromPreset(int heatTime, DateTimeOffset startHeatTime)
    {
        return new HeatingTimer()
        {
            Duration = new PresetHeatingDuration(heatTime),
            StartedDateTime = new HeatingStartDateTime(startHeatTime)
        };
    }
    
    public TimeSpan GetElapsedTime() => _stopwatch.Elapsed;
    
    public TimeSpan GetTimeRemaining() => TimeSpan.FromSeconds(Duration.Value - (int)_stopwatch.Elapsed.TotalSeconds);

    public void AddHeatingDuration(HeatingDuration heatingDuration)
    {
        StartedDateTime = new HeatingStartDateTime(DateTimeOffset.UtcNow);
        Duration += heatingDuration;
    }
    
    public bool IsHeating() => _stopwatch.IsRunning;


    public async Task StartTimerAsync(Func<Task> callback)
    {
        var tcs = new TaskCompletionSource();

        _stopwatch.Start();
        _timer = new Timer(async _ =>
        {
            if (_stopwatch.Elapsed.TotalSeconds >= Duration.Value)
            {
                DisposeTimer();
                tcs.TrySetResult();
                return;
            }

            if (callback != null)
            {
                try
                {
                    await callback();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                    DisposeTimer();
                }
            }
        }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

        await tcs.Task;
    }

    public void PauseTimer()
    {
        _stopwatch.Stop();
    }

    public void StopTimer()
    {
        Duration = CustomHeatingDuration.Create(1);
    }

    private void DisposeTimer()
    {
        _timer?.Dispose();
        _stopwatch.Stop();
    }
}