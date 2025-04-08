using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfo
    {
        public ReactiveProperty<string> Name { get; private set; }
        public ReactiveProperty<string> Description { get; private set; }
        public ReactiveProperty<Sprite> Icon { get; private set; }


        public UserInfo(string name, string description, Sprite icon)
        {
            Name = new ReactiveProperty<string>(name);
            Description = new ReactiveProperty<string>(description);
            Icon = new ReactiveProperty<Sprite>(icon);
        }

        [Button]
        public void ChangeName(string name)
        {
            Name.Value = name;
        }

        [Button]
        public void ChangeDescription(string description)
        {
            Description.Value = description;
        }

        [Button]
        public void ChangeIcon(Sprite icon)
        {
            Icon.Value = icon;
        }
    }
}
