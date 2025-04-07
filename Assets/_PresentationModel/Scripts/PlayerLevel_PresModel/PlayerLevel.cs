using System;
using Sirenix.OdinInspector;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevel
    {
        public event Action OnLevelUp;
        public event Action<int> OnExperienceChanged;

        [ShowInInspector] [ReadOnly] public int CurrentLevel { get; private set; } = 1;

        [ShowInInspector] [ReadOnly] public int CurrentExperience { get; private set; }

        [ShowInInspector]
        [ReadOnly]
        public int RequiredExperience
        {
            get { return 100 * (CurrentLevel + 1); }
        }

        [Button]
        public void AddExperience(int range)
        {
            var xp = Math.Min(CurrentExperience + range, RequiredExperience);
            CurrentExperience = xp;
            OnExperienceChanged?.Invoke(xp);
        }

        [Button]
        public void LevelUp()
        {
            if (CanLevelUp())
            {
                CurrentExperience = 0;
                CurrentLevel++;
                OnLevelUp?.Invoke();
            }
        }

        public bool CanLevelUp()
        {
            return CurrentExperience >= RequiredExperience;
        }
    }
}
