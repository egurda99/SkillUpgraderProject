using System;

namespace RealTimePractice
{
    [Serializable]
    public sealed class CrystallsChestReward : IChestReward
    {
        public int Crystalls;

        public IChestReward Clone()
        {
            return new CrystallsChestReward
            {
                Crystalls = Crystalls
            };
        }
    }
}
