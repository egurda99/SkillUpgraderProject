using System;

namespace RealTimePractice
{
    [Serializable]
    public sealed class MoneyChestReward : IChestReward
    {
        public int Money;

        public IChestReward Clone()
        {
            return new MoneyChestReward
            {
                Money = Money
            };
        }
    }
}
