using System;

namespace RealTimePractice
{
    [Serializable]
    public sealed class GemsChestReward : IChestReward
    {
        public int Gems;

        public IChestReward Clone()
        {
            return new GemsChestReward
            {
                Gems = Gems
            };
        }
    }
}
