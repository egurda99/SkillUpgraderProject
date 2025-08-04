using UnityEngine;
using UnityEngine.UI;

namespace _InventoryPractice.Game
{
    public class DragItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        private void LateUpdate()
        {
            transform.position = Input.mousePosition;
        }
    }
}
