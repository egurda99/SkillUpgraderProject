using System;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfoAdapter : IInitializable, IDisposable
    {
        private readonly UserInfo _userInfo;
        private readonly UserInfoView _userInfoView;

        public UserInfoAdapter(UserInfo userInfo, UserInfoView userInfoView)
        {
            _userInfo = userInfo;
            _userInfoView = userInfoView;
        }

        public void Initialize()
        {
            _userInfo.OnNameChanged += OnNameChanged;
            _userInfo.OnDescriptionChanged += OnDescriptionChanged;
            _userInfo.OnIconChanged += OnIconChanged;

            _userInfoView.SetupUser(_userInfo.Name, _userInfo.Description, _userInfo.Icon);
        }

        public void Dispose()
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
