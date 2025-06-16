using System.Collections.Generic;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public sealed class ZoneVisual : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _items;

        public void SetupItems(int currentAmount)
        {
            currentAmount = Mathf.Clamp(currentAmount, 0, _items.Count);

            for (var i = 0; i < currentAmount; i++)
            {
                var item = _items[i];
                item.SetActive(true);
            }

            var count = _items.Count;
            for (var i = currentAmount; i < count; i++)
            {
                var item = _items[i];
                item.SetActive(false);
            }
        }
    }
}
