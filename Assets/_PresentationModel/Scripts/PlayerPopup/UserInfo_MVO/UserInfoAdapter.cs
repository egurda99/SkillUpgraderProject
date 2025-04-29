using R3;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfoAdapter
    {
        private readonly UserInfo _userInfo;
        private readonly UserInfoView _userInfoView;

        private readonly CompositeDisposable _disposable = new();

        public UserInfoAdapter(UserInfo userInfo, UserInfoView userInfoView)
        {
            _userInfo = userInfo;
            _userInfoView = userInfoView;

            _userInfo.Name
                .Subscribe(OnNameChanged)
                .AddTo(_disposable);

            _userInfo.Description
                .Subscribe(OnDescriptionChanged)
                .AddTo(_disposable);

            _userInfo.Icon
                .Subscribe(OnIconChanged)
                .AddTo(_disposable);
        }

        // void IInitializable.Initialize()
        // {
        //     _userInfo.Name
        //         .Subscribe(OnNameChanged)
        //         .AddTo(_disposable);
        //
        //     _userInfo.Description
        //         .Subscribe(OnDescriptionChanged)
        //         .AddTo(_disposable);
        //
        //     _userInfo.Icon
        //         .Subscribe(OnIconChanged)
        //         .AddTo(_disposable);
        // }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public void Show()
        {
            _userInfoView.SetupUser(_userInfo.Name.CurrentValue, _userInfo.Description.CurrentValue,
                _userInfo.Icon.CurrentValue);
            _userInfoView.Show();
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

        public void Hide()
        {
            _userInfoView.Hide();
        }
    }
}
