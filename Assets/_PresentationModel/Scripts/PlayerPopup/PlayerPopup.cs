using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopup : Popup
    {
        [SerializeField] private PlayerLevelView _playerLevelView;
        [SerializeField] private Button _closeButton;


        private StatsListView _statListView;
        private UserInfoAdapter _userInfoAdapter;
        private PopupManager _popupManager;

        [Inject]
        public void Construct(StatsListView statListView, UserInfoAdapter userInfoAdapter, PopupManager popupManager)
        {
            _statListView = statListView;
            _userInfoAdapter = userInfoAdapter;
            _popupManager = popupManager;
        }

        protected override void OnShow()
        {
            _playerLevelView.Show();
            _statListView.Show();
            _userInfoAdapter.Show();
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        protected override void OnHide()
        {
            _playerLevelView.Hide();
            _statListView.Hide();
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }


        private void OnCloseButtonClicked()
        {
            _popupManager.HidePopup(_popupManager.FindName(this));
        }
    }
}
