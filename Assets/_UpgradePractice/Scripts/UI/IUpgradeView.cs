using UnityEngine;
using UnityEngine.Events;

namespace _UpgradePractice.Scripts
{
    public interface IUpgradeView
    {
        void AddButtonListener(UnityAction action);
        void RemoveButtonListener(UnityAction action);
        void SetTitleText(string title);
        void SetDescriptionText(string description);
        void SetCurrentStatText(string stat);
        void SetUpgradedStatText(string stat);
        void SetIconImage(Sprite icon);
        void SetPrice(string price);
        void SetButtonInteractable(bool interactable);
    }
}
