using System;
using UnityEngine;
using Zenject;

public class Timer : IInitializable, ITickable
{
    private float _interval;
    private float _time;
    private bool _isRunning;

    public event Action OnElapsed;


    void IInitializable.Initialize()
    {
        _time = 0;
    }

    public void Start() => _isRunning = true;
    public void Stop() => _isRunning = false;
    public void Reset() => _time = 0f;

    public void SetInterval(float interval)
    {
        _interval = interval;
    }


    public void Tick()
    {
        if (!_isRunning)
            return;
        _time += Time.deltaTime;
        if (_time >= _interval)
        {
            _time = 0f;
            OnElapsed?.Invoke();
        }
    }
}
