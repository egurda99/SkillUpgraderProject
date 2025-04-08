using TMPro;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class StatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _value;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }


        public void SetupStat(string name, string value)
        {
            Setuper(name, value);
        }

        public void UpdateStat(string name, string value)
        {
            Setuper(name, value);
        }

        public void UpdateStatValue(string value)
        {
            _value.text = value;
        }

        public void UpdateStatName(string name)
        {
            _name.text = name;
        }

        private void Setuper(string name, string value)
        {
            _name.text = name;
            _value.text = value;
        }
    }
}
