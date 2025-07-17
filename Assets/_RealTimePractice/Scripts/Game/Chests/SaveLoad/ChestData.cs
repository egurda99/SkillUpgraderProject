using System;

namespace RealTimePractice
{
    [Serializable]
    public sealed class ChestData
    {
        public ChestType ChestType;
        public string Id;
        public string SaveTime;
        public float OpenDuration;
        public float RemainingTime;

        public ChestData(ChestType chestType, string id, string saveTime, float openDuration, float remainingTime)
        {
            ChestType = chestType;
            Id = id;
            SaveTime = saveTime;
            OpenDuration = openDuration;
            RemainingTime = remainingTime;
        }
    }
}
