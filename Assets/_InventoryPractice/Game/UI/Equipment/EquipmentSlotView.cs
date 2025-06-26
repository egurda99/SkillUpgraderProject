using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _InventoryPractice
{
    [RequireComponent(typeof(Button))]
    public sealed class EquipmentSlotView : MonoBehaviour, IEquipmentSlotView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _defaultIcon;
        [SerializeField] private Button _button;


        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
            _defaultIcon.enabled = false;
            _icon.enabled = true;
        }

        public void SetDefaultSprite()
        {
            _defaultIcon.enabled = true;
            _icon.enabled = false;
        }


        public void AddButtonListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void RemoveButtonListener(UnityAction action)
        {
            _button.onClick.RemoveListener(action);
        }

        public void RemoveAllButtonListeners()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
