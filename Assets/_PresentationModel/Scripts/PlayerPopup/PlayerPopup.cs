using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    [RequireComponent(typeof(PlayerPopupView))]
    public sealed class PlayerPopup : Popup
    {
        private PlayerPopupView _popupView;

        private PlayerPopupViewModel _viewModel;


        private PlayerPopupViewModelFactory _playerPopupViewModelFactory;

        private void Awake()
        {
            _popupView = GetComponent<PlayerPopupView>();
        }

        [Inject]
        public void Construct(PlayerPopupViewModelFactory playerPopupViewModelFactory)
        {
            _playerPopupViewModelFactory = playerPopupViewModelFactory;
        }

        protected override void OnShow()
        {
            _viewModel = _playerPopupViewModelFactory.Create();
            _popupView.Show(_viewModel);
        }

        protected override void OnHide()
        {
            _popupView.Hide();
        }

        private void OnDestroy()
        {
            _viewModel?.Dispose();
        }
    }
}
