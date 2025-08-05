using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _InventoryPractice
{
    [RequireComponent(typeof(Button))]
    public sealed class InventorySlotView : MonoBehaviour, IInventorySlotView, IBeginDragHandler, IDragHandler,
        IEndDragHandler, IDropHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amountText;

        [SerializeField] private Button _button;

        public event UnityAction<PointerEventData> BeginDragEvent;
        public event UnityAction<PointerEventData> EndDragEvent;
        public event UnityAction<PointerEventData> DropEvent;

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDragEvent?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDragEvent?.Invoke(eventData);
        }

        public void OnDrop(PointerEventData eventData)
        {
            DropEvent?.Invoke(eventData);
        }


        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
            _icon.enabled = sprite != null;
        }

        public void SetAmount(string value)
        {
            _amountText.text = value;
            _amountText.enabled = !string.IsNullOrEmpty(value);
        }

        public void AddButtonListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void RemoveButtonListener(UnityAction action)
        {
            _button.onClick.RemoveListener(action);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }
    }
}
