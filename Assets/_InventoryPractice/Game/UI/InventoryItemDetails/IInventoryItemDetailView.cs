using MyCodeBase.UI;
using UnityEngine;
using UnityEngine.Events;

namespace _InventoryPractice
{
    public interface IInventoryItemDetailView
    {
        void Show();
        void Hide();
        void SetName(string name);
        void SetAmount(string amount);
        void SetDescription(string description);
        void SetWeight(string weight);
        void SetIcon(Sprite icon);
        void ShowUseButton(bool show);
        void ShowEquipButton(bool show);
        void ShowUnEquipButton(bool show);
        void ShowDropButton(bool show);
        void SetUseActionListener(UnityAction action);
        void SetEquipActionListener(UnityAction action);
        void SetDropActionListener(UnityAction action);
        void SetUnEquipActionListener(UnityAction action);
        void RemoveUseActionListener(UnityAction action);
        void RemoveEquipActionListener(UnityAction action);
        void RemoveDropActionListener(UnityAction action);
        void RemoveUnEquipActionListener(UnityAction action);
        void SetTweenManager(DoTweenAnimationManager tweenAnimationManager);
    }
}
