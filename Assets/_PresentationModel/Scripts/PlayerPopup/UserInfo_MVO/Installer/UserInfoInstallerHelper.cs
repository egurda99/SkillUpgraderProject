using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfoInstallerHelper : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _iconSprite;

        [SerializeField] private UserInfoView _userInfoView;


        public string Name => _name;

        public string Description => _description;

        public Sprite IconSprite => _iconSprite;

        public UserInfoView UserInfoView => _userInfoView;
    }
}
