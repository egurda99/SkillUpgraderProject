using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopup : Popup
    {
        [SerializeField] private PlayerPopupView _popupView;

        private PlayerPopupViewModel _viewModel;


        private PlayerPopupViewModelFactory _playerPopupViewModelFactory;


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
