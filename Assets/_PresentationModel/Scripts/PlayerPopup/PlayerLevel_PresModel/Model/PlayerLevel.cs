using System;
using R3;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevel
    {
        public ReactiveProperty<int> CurrentExperienceProperty = new(0);
        public ReactiveProperty<int> CurrentLevelProperty = new(1);


        public int RequiredExperience
        {
            get { return 100 * (CurrentLevelProperty.CurrentValue + 1); }
        }

        public void AddExperience(int range)
        {
            var xp = Math.Min(CurrentExperienceProperty.Value + range, RequiredExperience);
            CurrentExperienceProperty.Value = xp;
        }

        public void LevelUp()
        {
            if (CanLevelUp())
            {
                CurrentExperienceProperty.Value = 0;
                CurrentLevelProperty.Value++;
            }
        }

        public bool CanLevelUp()
        {
            return CurrentExperienceProperty.CurrentValue >= RequiredExperience;
        }
    }
}
