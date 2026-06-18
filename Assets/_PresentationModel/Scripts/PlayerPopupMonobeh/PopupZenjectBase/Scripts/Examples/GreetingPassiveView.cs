using TMPro;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class GreetingPassiveView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _messageText;

        public void SetMessage(string message) => _messageText.text = message;
    }
}
