using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _InventoryPractice
{
    public sealed class InventoryItemDetailView : MonoBehaviour, IInventoryItemDetailView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _amountText;

        [SerializeField] private Button _useButton;
        [SerializeField] private Button _equipButton;
        [SerializeField] private Button _dropButton;


        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetName(string name)
        {
            _nameText.text = name;
        }

        public void SetAmount(string amount)
        {
            _amountText.text = amount;
        }

        public void SetDescription(string description)
        {
            _descriptionText.text = description;
        }

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void ShowUseButton(bool show)
        {
            _useButton.gameObject.SetActive(show);
        }

        public void ShowEquipButton(bool show)
        {
            _equipButton.gameObject.SetActive(show);
        }

        public void ShowDropButton(bool show)
        {
            _dropButton.gameObject.SetActive(show);
        }

        public void SetUseActionListener(UnityAction action)
        {
            _useButton.onClick.AddListener(action);
        }

        public void SetEquipActionListener(UnityAction action)
        {
            _equipButton.onClick.AddListener(action);
        }

        public void SetDropActionListener(UnityAction action)
        {
            _dropButton.onClick.AddListener(action);
        }

        public void RemoveUseActionListener(UnityAction action)
        {
            _useButton.onClick.RemoveListener(action);
        }

        public void RemoveEquipActionListener(UnityAction action)
        {
            _equipButton.onClick.RemoveListener(action);
        }

        public void RemoveDropActionListener(UnityAction action)
        {
            _dropButton.onClick.RemoveListener(action);
        }
    }
}
