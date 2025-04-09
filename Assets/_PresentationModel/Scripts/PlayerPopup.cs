using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopup : Popup
    {
        [SerializeField] private PlayerLevelView _playerLevelView;

        [SerializeField] private Button _closeButton;


        private StatListViewAdapter _statListViewAdapter;
        private UserInfoAdapter _userInfoAdapter;
        private PlayerPresentationModel _playerPresentaionModel;
        private PopupManager _popupManager;

        [Inject]
        public void Construct(StatListViewAdapter statListViewAdapter, UserInfoAdapter userInfoAdapter,
            PlayerPresentationModel playerPresentationModel, PopupManager popupManager)
        {
            _statListViewAdapter = statListViewAdapter;
            _userInfoAdapter = userInfoAdapter;
            _playerPresentaionModel = playerPresentationModel;
            _popupManager = popupManager;
        }

        protected override void OnShow()
        {
            _playerLevelView.Show(_playerPresentaionModel);
            _statListViewAdapter.Show();
            _userInfoAdapter.Show();
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        protected override void OnHide()
        {
            _playerLevelView.Hide();
            _statListViewAdapter.Hide();
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }


        private void OnCloseButtonClicked()
        {
            _popupManager.HidePopup(_popupManager.FindName(this));
        }
    }
}
