using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RealTimePractice
{
    public sealed class AsyncSessionManager : ISessionManager
    {
        private readonly IAsyncTimeProvider _timeProvider;
        private readonly List<GameSession> _sessions = new();
        private GameSession _currentSession;
        private readonly SaveLoadManager _saveLoadManager;

        public List<GameSession> GameSessions => _sessions;

        public AsyncSessionManager(IAsyncTimeProvider timeProvider, SaveLoadManager saveLoadManager)
        {
            _timeProvider = timeProvider;
            _saveLoadManager = saveLoadManager;
        }

        public void SetupSessions(List<SessionData> dataSessionsDataList)
        {
            _sessions.Clear();
            foreach (var dataSession in dataSessionsDataList)
            {
                _sessions.Add(new GameSession(dataSession.StartTime, dataSession.EndTime));
            }
        }


        public void StartSession()
        {
            StartSessionInternal().Forget();
        }

        public void EndSession()
        {
            EndSessionInternal().Forget();
        }


        public async UniTask EndSessionAsync()
        {
            if (_currentSession == null) return;

            var endTime = await _timeProvider.GetCurrentTimeAsync();
            _currentSession.End(endTime);
            _sessions.Add(_currentSession);
            _saveLoadManager.Save();

            Debug.Log(
                $"[AsyncSessionManager] Ended session at {endTime}, duration: {_currentSession.Duration.TotalMinutes:F1} min");
            _currentSession = null;
        }

        private async UniTaskVoid StartSessionInternal()
        {
            var time = await _timeProvider.GetCurrentTimeAsync();
            _currentSession = new GameSession(time);
            Debug.Log($"[AsyncSessionManager] Started session at {time}");
        }

        private async UniTaskVoid EndSessionInternal()
        {
            if (_currentSession == null) return;

            var time = await _timeProvider.GetCurrentTimeAsync();
            _currentSession.End(time);
            _sessions.Add(_currentSession);
            _saveLoadManager.Save();

            Debug.Log(
                $"[AsyncSessionManager] Ended session at {time}, duration: {_currentSession.Duration.TotalMinutes:F1} min");
            _currentSession = null;
        }
    }
}
