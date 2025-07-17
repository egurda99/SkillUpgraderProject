using System.Collections.Generic;
using UnityEngine;

namespace RealTimePractice
{
    public class SyncSessionManager : ISessionManager
    {
        private readonly ISyncTimeProvider _timeProvider;
        private readonly List<GameSession> _sessions = new();
        private GameSession _currentSession;
        private readonly SaveLoadManager _saveLoadManager;

        public List<GameSession> GameSessions => _sessions;

        public SyncSessionManager(ISyncTimeProvider syncTimeProvider, SaveLoadManager saveLoadManager)
        {
            _timeProvider = syncTimeProvider;
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
            _currentSession = new GameSession(_timeProvider.GetCurrentTime());
            Debug.Log($"[SyncSessionManager] Started session at {_currentSession.StartTime}");
        }

        public void EndSession()
        {
            if (_currentSession == null) return;

            _currentSession.End(_timeProvider.GetCurrentTime());
            _sessions.Add(_currentSession);
            // _storage.SaveSessions(_sessions);
            _saveLoadManager.Save();

            Debug.Log(
                $"[SyncSessionManager] Ended session at {_currentSession.EndTime}, duration: {_currentSession.Duration.TotalMinutes:F1} min");
            _currentSession = null;
        }
    }
}
