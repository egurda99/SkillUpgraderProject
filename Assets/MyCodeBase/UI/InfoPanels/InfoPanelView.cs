using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyCodeBase.UI
{
    public sealed class InfoPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        [SerializeField] private Image _iconImage;

        public void SetTitle(string title)
        {
            _titleText.text = title;
        }

        public void SetIcon(Sprite icon)
        {
            _iconImage.sprite = icon;
        }

        public void SetDescription(string description)
        {
            _descriptionText.text = description;
        }
    }
}