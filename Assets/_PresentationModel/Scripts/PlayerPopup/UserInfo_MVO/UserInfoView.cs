using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _icon;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }


        public void SetupUser(string userName, string description, Sprite icon)
        {
            Setter(userName, description, icon);
        }

        public void UpdateUser(string userName, string description, Sprite icon)
        {
            Setter(userName, description, icon);
        }

        public void SetName(string name)
        {
            _name.text = name;
        }

        public void SetDescription(string description)
        {
            _description.text = description;
        }

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        private void Setter(string userName, string description, Sprite icon)
        {
            _name.text = userName;
            _description.text = description;
            _icon.sprite = icon;
        }
    }
}
