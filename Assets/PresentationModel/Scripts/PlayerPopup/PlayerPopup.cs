using Zenject;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopup : Popup
    {
        private PlayerPopupViewModel _playerPopupViewModel;

        [Inject]
        public void Construct(PlayerPopupSectionsFactory playerPopupSectionsFactory)
        {
            _playerPopupViewModel = new PlayerPopupViewModel(playerPopupSectionsFactory);
        }

        protected override void OnShow()
        {
            _playerPopupViewModel.Show();
        }

        protected override void OnHide()
        {
            _playerPopupViewModel.Hide();
        }

        private void OnDestroy()
        {
            _playerPopupViewModel.Dispose();
        }
    }
}
