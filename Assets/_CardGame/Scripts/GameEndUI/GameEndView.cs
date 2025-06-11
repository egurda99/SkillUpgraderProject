using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _CardGame
{
    public sealed class GameEndView : MonoBehaviour
    {
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private Image _image;


        [SerializeField] private TextMeshProUGUI _winText;

        private void Awake()
        {
            Hide();
        }

        public void Show()
        {
            _winPanel.SetActive(true);
        }

        public void Hide()
        {
            _winPanel.SetActive(false);
        }

        public void SetWinText(string text)
        {
            _winText.text = text;
        }

        public void SetWinColor(Color color)
        {
            _image.color = color;
        }
    }
}
