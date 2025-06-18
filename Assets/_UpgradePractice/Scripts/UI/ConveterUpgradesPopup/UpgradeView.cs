using MyCodeBase;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _UpgradePractice.Scripts
{
    public sealed class UpgradeView : MonoBehaviour, IUpgradeView
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _currentStatText;
        [SerializeField] private TextMeshProUGUI _upgradedStatText;

        [SerializeField] private Image _iconImage;
        [SerializeField] private BuyButton _buyButton;

        public void AddButtonListener(UnityAction action)
        {
            _buyButton.AddListener(action);
        }

        public void RemoveButtonListener(UnityAction action)
        {
            _buyButton.RemoveListener(action);
        }

        public void SetTitleText(string title)
        {
            _titleText.text = title;
        }

        public void SetDescriptionText(string description)
        {
            _descriptionText.text = description;
        }

        public void SetCurrentStatText(string stat)
        {
            _currentStatText.text = stat;
        }

        public void SetUpgradedStatText(string stat)
        {
            _upgradedStatText.text = stat;
        }

        public void SetIconImage(Sprite icon)
        {
            _iconImage.sprite = icon;
        }


        public void SetPrice(string price)
        {
            _buyButton.SetPrice(price);
        }

        public void SetButtonInteractable(bool interactable)
        {
            _buyButton.SetAvailable(interactable);
        }
    }
}
