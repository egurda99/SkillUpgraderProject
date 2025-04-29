using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopup : Popup
    {
        [SerializeField] private PlayerLevelView _playerLevelView;

        private StatsListView _statListView;
        private UserInfoAdapter _userInfoAdapter;

        [Inject]
        public void Construct(StatsListView statListView, UserInfoAdapter userInfoAdapter)
        {
            _statListView = statListView;
            _userInfoAdapter = userInfoAdapter;
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
        }
    }
}
