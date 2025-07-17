using UnityEngine;
using Zenject;

namespace RealTimePractice
{
    public sealed class SessionModule : MonoBehaviour
    {
        private ISessionManager _sessionManager;
        private bool _isInitialized;
        private bool _isInSession;
        private SaveLoadManager _saveLoadManager;

        public ISessionManager GetManager() => _sessionManager;

        [Inject]
        public void Construct(SaveLoadManager saveLoadManager, ISessionManager sessionManager)
        {
            _saveLoadManager = saveLoadManager;
            _sessionManager = sessionManager;
        }

        private void Start()
        {
            _saveLoadManager.Load();

            _sessionManager.StartSession();

            _isInitialized = true;
            _isInSession = true;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!_isInitialized)
                return;

            if (hasFocus && !_isInSession)
            {
                StartSessionSafe();
                _isInSession = true;
            }
            else if (!hasFocus && _isInSession)
            {
                EndSessionSafe();
                _isInSession = false;
            }
        }

        private void StartSessionSafe()
        {
            if (_sessionManager is AsyncSessionManager asyncManager)
                asyncManager.StartSession();
            else
                _sessionManager.StartSession();
        }

        private void EndSessionSafe()
        {
            if (_sessionManager is AsyncSessionManager asyncManager)
                asyncManager.EndSessionAsync(); // Fire-and-forget
            else
                _sessionManager.EndSession();
        }
    }
}
