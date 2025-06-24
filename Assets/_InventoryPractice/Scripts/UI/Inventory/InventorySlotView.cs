using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _InventoryPractice
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class InventorySlotView : MonoBehaviour, IInventorySlotView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amountText;

        [SerializeField] private Button _button;


        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void SetAmount(string value)
        {
            _amountText.text = value;
        }

        public void AddButtonListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void RemoveButtonListener(UnityAction action)
        {
            _button.onClick.RemoveListener(action);
        }

        public void Clear()
        {
            _icon.sprite = null;
            _icon.enabled = false;
        }
    }
}
