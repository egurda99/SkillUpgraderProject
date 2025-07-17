using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RealTimePractice
{
    public sealed class Chest
    {
        private ChestConfig _config;

        private readonly string _id;
        private readonly float _duration;
        [ShowInInspector] [ReadOnly] private Countdown _timer = new();
        private readonly ChestType _chestType;


        public ChestType ChestType => _chestType;
        public float Duration => _duration;
        public string ID => _id;

        public float RemainingTime => _timer.CurrentTime;

        public event Action<Chest> OnStarted;
        public event Action<Chest> OnOpened;

        public Chest(ChestConfig config, string id)
        {
            _config = config;
            _chestType = config.ChestType;
            _id = id;
            _duration = config.Duration;
            _timer.Duration = _duration;
            Start();
        }

        public Chest(ChestType chestType, string id, float duration)
        {
            _chestType = chestType;
            _id = id;
            _duration = duration;
            _timer.Duration = _duration;
            Start();
        }

        public void Start()
        {
            if (_timer.Progress <= 0)
            {
                OnStarted?.Invoke(this);
            }

            _timer.Play();
        }

        public void FirstStart()
        {
            _timer.ResetTime();
            _timer.Play();
        }


        public void Update(float deltaTime)
        {
            _timer.Tick(deltaTime);
        }

        [Button]
        public bool CanReceiveReward()
        {
            return _timer.Progress >= 1;
        }

        [Button]
        public void ReceiveReward()
        {
            if (!CanReceiveReward())
            {
                Debug.LogError("Can't receive reward!");
                return;
            }

            _timer.ResetTime();
            _timer.Play();
            OnStarted?.Invoke(this);
            OnOpened?.Invoke(this);

            Debug.Log("<color=orange>Reward gathered</color>");
        }

        public void Synchronize(float offlineSeconds)
        {
            _timer.SetCurrentTime(offlineSeconds);
        }
    }
}
