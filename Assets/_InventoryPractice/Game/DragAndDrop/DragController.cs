using InventoryPractice;
using UnityEngine;

namespace _InventoryPractice.Game
{
    public class DragController : MonoBehaviour
    {
        public static DragController Instance;

        [SerializeField] private DragItemView _dragItemViewPrefab;
        [SerializeField] private Transform _container;

        private DragItemView _currentView;

        public InventoryItem DraggedItem { get; private set; }
        public bool HasItem => DraggedItem != null;

        private void Awake()
        {
            Instance = this;
        }

        public void StartDrag(InventoryItem item, Sprite icon)
        {
            Debug.Log($"<color=red>Started: {item.Id}</color>");
            DraggedItem = item;
            // _currentView = Instantiate(_dragItemViewPrefab, transform);
            _currentView = Instantiate(_dragItemViewPrefab, _container);
            _currentView.SetIcon(icon);
        }

        public void EndDrag()
        {
            Debug.Log($"<color=red>Ended: {_currentView}</color>");

            DraggedItem = null;
            if (_currentView != null)
                Destroy(_currentView.gameObject);
        }
    }
}
