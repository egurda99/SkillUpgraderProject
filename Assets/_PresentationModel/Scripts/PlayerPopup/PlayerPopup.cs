using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopup : Popup
    {
        [SerializeField] private PlayerLevelView _playerLevelView;
        [SerializeField] private UserInfoView _userInfoView;


        private StatListView _statListView;
        private UserInfoAdapter _userInfoAdapter;

        [Inject]
        public void Construct(StatListView statListView, UserInfo userInfo)
        {
            _statListView = statListView; // sozdaet staty
            _userInfoAdapter = new UserInfoAdapter(userInfo, _userInfoView);
        }

        protected override void OnShow()
        {
            _playerLevelView.Show();
            _statListView.Show();
            _userInfoAdapter.Show();
        }

        protected override void OnHide()
        {
            _playerLevelView.Hide();
            _statListView.Hide();
            _userInfoAdapter.Hide();
        }

        private void OnDestroy()
        {
            _userInfoAdapter?.Dispose();
        }
    }
}
