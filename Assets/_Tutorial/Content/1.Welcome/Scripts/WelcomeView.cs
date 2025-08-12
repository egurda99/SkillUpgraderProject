using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Tutorial
{
    public sealed class WelcomeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;

        [SerializeField] private TextMeshProUGUI _descriptionText;

        [SerializeField] private Button _startButton;

        public void AddButtonListener(UnityAction action)
        {
            _startButton.onClick.AddListener(action);
        }

        public void RemoveButtonListener(UnityAction action)
        {
            _startButton.onClick.RemoveListener(action);
        }

        public void SetTitle(string title)
        {
            _titleText.text = title;
        }

        public void SetDescription(string description)
        {
            _descriptionText.text = description;
        }
    }
}
