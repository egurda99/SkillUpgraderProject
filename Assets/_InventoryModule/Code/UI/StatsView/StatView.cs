using TMPro;
using UnityEngine;

namespace _InventoryPractice
{
    public sealed class StatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}