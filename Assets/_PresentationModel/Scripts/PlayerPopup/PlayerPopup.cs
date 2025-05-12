using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopup : Popup
    {
        private PlayerPopupViewModel _playerPopupViewModel;

        protected override void OnShow()
        {
            if (ViewModel is PlayerPopupViewModel playerViewModel)
            {
                _playerPopupViewModel = playerViewModel;
                _playerPopupViewModel.Show();
            }
            else
            {
                Debug.LogError("Invalid ViewModel passed to PlayerPopup");
            }
        }

        protected override void OnHide()
        {
            _playerPopupViewModel?.Hide();
        }

        protected override void OnDestroy()
        {
            _playerPopupViewModel?.Dispose();
        }
    }
}
