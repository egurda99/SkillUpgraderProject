using System;
using ShootEmUp;
using UnityEngine;

public sealed class TimerBeforeStartWidget : MonoBehaviour, IGameFinishListener,
    IGameFixedUpdateListener
{
    [SerializeField] private GameObject _number3;
    [SerializeField] private GameObject _number2;
    [SerializeField] private GameObject _number1;

    private Timer _timer;
    private bool _isWorking;

    private bool _isNumber3Active;
    private bool _isNumber2Active;
    private bool _isNumber1Active;


    public event Action OnTimerEnded;

    private void Awake()
    {
        _timer = new Timer();
        _timer.OnTimerEnd += TimerEnded;
        Hide();
    }

    void IGameFinishListener.OnFinishGame()
    {
        _timer.OnTimerEnd -= TimerEnded;
    }

    void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
    {
        if (!_isWorking)
            return;

        _timer.UpdateTimer(fixedDeltaTime);
    }

    public void StartTimer()
    {
        _isWorking = true;
        _number3.SetActive(true);
        _isNumber3Active = true;
        _timer.StartTimer(1);
    }


    private void TimerEnded()
    {
        if (_isNumber3Active)
        {
            _isNumber3Active = false;
            _number3.SetActive(false);
            _isNumber2Active = true;
            _number2.SetActive(true);
            _timer.StartTimer(1);
        }

        else if (_isNumber2Active)
        {
            _isNumber2Active = false;
            _number2.SetActive(false);
            _isNumber1Active = true;
            _number1.SetActive(true);
            _timer.StartTimer(1);
        }

        else if (_isNumber1Active)
        {
            _isNumber1Active = false;
            _number1.SetActive(false);
            _isWorking = false;

            OnTimerEnded?.Invoke();
        }
    }

    private void Hide()
    {
        _number3.SetActive(false);
        _number2.SetActive(false);
        _number1.SetActive(false);
    }
}