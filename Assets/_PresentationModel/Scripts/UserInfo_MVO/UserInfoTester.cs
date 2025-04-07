using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfoTester : MonoBehaviour
    {
        // [SerializeField] private string _name;
        // [SerializeField] private string _description;
        // [SerializeField] private Sprite _iconSprite;

        private UserInfo _userInfo;

        [Inject]
        public void Construct(UserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        [Button]
        public void ChangeName(string name)
        {
            _userInfo.ChangeName(name);
        }

        [Button]
        public void ChangeDescription(string description)
        {
            _userInfo.ChangeDescription(description);
        }

        [Button]
        public void ChangeIcon(Sprite icon)
        {
            _userInfo.ChangeIcon(icon);
        }
    }
}
