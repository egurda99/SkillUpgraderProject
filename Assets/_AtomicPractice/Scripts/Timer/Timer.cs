using System;

public class Timer
{
    private float _interval;
    private float _time;
    private bool _isRunning;

    public event Action OnElapsed;

    public Timer(float interval)
    {
        _interval = interval;
        _time = 0f;
        _isRunning = true;
    }


    public void Start() => _isRunning = true;
    public void Stop() => _isRunning = false;
    public void Reset() => _time = 0f;

    public void SetInterval(float interval) => _interval = interval;

    public void Update(float deltaTime)
    {
        if (!_isRunning)
            return;

        _time += deltaTime;
        if (_time >= _interval)
        {
            _time = 0f;
            OnElapsed?.Invoke();
        }
    }
}
