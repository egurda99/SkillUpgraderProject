using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace RealTimePractice.UI
{
    public sealed class SessionsListView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private SessionView _viewPrefab;

        private readonly List<ViewHolder> _viewHolders = new();

        private ISessionManager _sessionManager;

        [Inject]
        public void Construct(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        private void Start()
        {
            Show();
        }

        [Button]
        public void Show()
        {
            var sessions = _sessionManager.GameSessions;
            for (int i = 0, count = sessions.Count; i < count; i++)
            {
                var session = sessions[i];
                ShowSession(session, i);
            }
        }

        [Button]
        public void Hide()
        {
            for (int i = 0, count = _viewHolders.Count; i < count; i++)
            {
                var vh = _viewHolders[i];
                HideSession(vh);
            }

            _viewHolders.Clear();
        }

        private void ShowSession(GameSession session, int index)
        {
            var view = Instantiate(_viewPrefab, _container);
            var adapter = new SessionViewAdapter(view, session, index);
            adapter.Show();

            _viewHolders.Add(new ViewHolder(view, adapter));
        }

        private void HideSession(ViewHolder vh)
        {
            Destroy(vh.View.gameObject);
        }

        private readonly struct ViewHolder
        {
            public readonly SessionView View;
            public readonly SessionViewAdapter Adapter;

            public ViewHolder(SessionView view, SessionViewAdapter adapter)
            {
                View = view;
                Adapter = adapter;
            }
        }
    }
}
