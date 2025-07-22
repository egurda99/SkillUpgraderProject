using UnityEngine;
using UnityEngine.UI;

namespace _UpgradePractice.Scripts
{
    public sealed class ProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private Image _fillImage;

        public void SetVisible(bool isVisible)
        {
            _root.SetActive(isVisible);
        }

        public void SetProgress(float progress)
        {
            _fillImage.fillAmount = progress;
        }
    }
}
