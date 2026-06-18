using TMPro;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsPassiveView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _statsText;

        public void SetStats(string text)
        {
            _statsText.text = text;
        }
    }
}
