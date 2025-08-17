using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _InventoryPractice
{
    public sealed class DragItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amountText;


        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetAmount(string amount)
        {
            _amountText.text = amount;
        }


        private void LateUpdate()
        {
            transform.position = Input.mousePosition;
        }
    }
}
