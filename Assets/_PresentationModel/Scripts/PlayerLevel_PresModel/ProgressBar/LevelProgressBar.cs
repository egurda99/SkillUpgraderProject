using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class LevelProgressBar : MonoBehaviour
    {
        [Space] [SerializeField] private Image _buttonBackground;

        [SerializeField] private Sprite _readyForUpgradeSprite;

        [SerializeField] private Sprite _lockedUpgradeSprite;

        public void SetStatus(bool isAvailable)
        {
            _buttonBackground.sprite = isAvailable ? _readyForUpgradeSprite : _lockedUpgradeSprite;
        }
    }
}
