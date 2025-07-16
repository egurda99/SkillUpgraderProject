using System;
using System.Collections.Generic;

namespace RealTimePractice
{
    [Serializable]
    public sealed class SessionsData
    {
        public List<SessionData> SessionsDataList = new();


        public SessionsData(List<SessionData> sessionsDataList)
        {
            SessionsDataList.AddRange(sessionsDataList);
        }
    }
}
