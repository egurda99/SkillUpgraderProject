using System;
using System.Collections.Generic;

namespace RealTimePractice
{
    [Serializable]
    public sealed class ChestsData
    {
        public List<ChestData> ChestsDataList = new();

        public ChestsData(List<ChestData> chestsDataList)
        {
            if (chestsDataList != null)
            {
                ChestsDataList.AddRange(chestsDataList);
            }
        }
    }
}
