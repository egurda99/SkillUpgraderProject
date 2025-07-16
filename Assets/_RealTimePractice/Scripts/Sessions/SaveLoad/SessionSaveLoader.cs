using System;
using System.Collections.Generic;
using UnityEngine;

namespace RealTimePractice
{
    [Serializable]
    public class SessionSaveLoader : SaveLoader<ISessionManager, SessionsData>
    {
        protected override SessionsData ConvertToData(ISessionManager service)
        {
            var sessionsData = new List<SessionData>();

            foreach (var session in service.GameSessions)
            {
                sessionsData.Add(new SessionData(session.StartTime, session.EndTime));
            }

            return new SessionsData(sessionsData);
        }

        protected override void SetupData(ISessionManager service, SessionsData data)
        {
            service.SetupSessions(data.SessionsDataList);
            Debug.Log($"<color=yellow>Setup data = {data.SessionsDataList}</color>");
            Debug.Log($"<color=yellow>Amount Sessions = {data.SessionsDataList.Count}</color>");
        }

        protected override void SetupDefaultData(ISessionManager service)
        {
            base.SetupDefaultData(service);
            Debug.Log("[SessionStorage] No session file found, returning empty list.");
        }
    }
}
