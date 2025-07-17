using System.Collections.Generic;

namespace RealTimePractice
{
    public interface ISessionManager
    {
        void StartSession();
        void EndSession();
        List<GameSession> GameSessions { get; }
        void SetupSessions(List<SessionData> dataSessionsDataList);
    }
}
