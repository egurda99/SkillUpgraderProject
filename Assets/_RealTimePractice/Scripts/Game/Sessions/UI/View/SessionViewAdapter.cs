namespace RealTimePractice.UI
{
    public sealed class SessionViewAdapter
    {
        private readonly SessionView _sessionView;
        private readonly GameSession _gameSession;
        private readonly int _numberOfSession;

        public SessionViewAdapter(SessionView sessionView, GameSession gameSession, int numberOfSession)
        {
            _sessionView = sessionView;
            _gameSession = gameSession;
            _numberOfSession = numberOfSession;
        }

        public void Show()
        {
            _sessionView.SetSessionNumberText("#" + (_numberOfSession + 1));
            _sessionView.SetStartTimeText(_gameSession.StartTime.ToString());
            _sessionView.SetEndTimeText(_gameSession.EndTime.ToString());
            _sessionView.SetDurationTimeText($"{_gameSession.Duration.TotalSeconds:F1} sec");
        }
    }
}
