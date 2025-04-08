using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfoAdapter
    {
        private readonly UserInfo _userInfo;
        private readonly UserInfoView _userInfoView;

        public UserInfoAdapter(UserInfo userInfo, UserInfoView userInfoView)
        {
            _userInfo = userInfo;
            _userInfoView = userInfoView;
        }

        public void Show()
        {
            _userInfo.OnNameChanged += OnNameChanged;
            _userInfo.OnDescriptionChanged += OnDescriptionChanged;
            _userInfo.OnIconChanged += OnIconChanged;

            _userInfoView.SetupUser(_userInfo.Name, _userInfo.Description, _userInfo.Icon);
        }

        public void Hide()
        {
            _userInfo.OnNameChanged -= OnNameChanged;
            _userInfo.OnDescriptionChanged -= OnDescriptionChanged;
            _userInfo.OnIconChanged -= OnIconChanged;
        }

        private void OnIconChanged(Sprite sprite)
        {
            _userInfoView.SetIcon(sprite);
        }

        private void OnDescriptionChanged(string description)
        {
            _userInfoView.SetDescription(description);
        }

        private void OnNameChanged(string name)
        {
            _userInfoView.SetName(name);
        }
    }
}
