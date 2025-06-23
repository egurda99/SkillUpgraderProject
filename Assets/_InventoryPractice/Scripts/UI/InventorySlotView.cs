using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _InventoryPractice
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class InventorySlotView : MonoBehaviour, IInventorySlotView,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amountText;
        private CanvasGroup _canvasGroup;
        private Button _button;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _button = GetComponent<Button>();
        }

        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
            _icon.enabled = true;
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

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 0.6f;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}
