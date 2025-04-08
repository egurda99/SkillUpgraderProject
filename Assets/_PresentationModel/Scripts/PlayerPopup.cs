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


        private void Awake()
        {
            _popupManager = FindObjectOfType<PopupManager>();
        }

        [Inject]
        public void Construct(StatListViewAdapter statListViewAdapter, UserInfoAdapter userInfoAdapter,
            PlayerPresentationModel playerPresentationModel)
        {
            _statListViewAdapter = statListViewAdapter;
            _userInfoAdapter = userInfoAdapter;
            _playerPresentaionModel = playerPresentationModel;
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
            _userInfoAdapter.Hide();
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }


        private void OnCloseButtonClicked()
        {
            _popupManager.HidePopup(_popupManager.FindName(this));
        }
    }
}
