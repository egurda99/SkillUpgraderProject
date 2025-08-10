using System;
using Audio;
using InventoryPractice;
using MyCodeBase.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _InventoryPractice
{
    [RequireComponent(typeof(Button))]
    public sealed class EquipmentSlotView : MonoBehaviour, IEquipmentSlotView, IBeginDragHandler, IDragHandler,
        IEndDragHandler, IDropHandler
    {
        [SerializeField] private ImageEquipmentSlot _imageEquipmentSlot;

        [SerializeField] private Button _button;
        [SerializeField] private string _clickClipName;
        [SerializeField] private string _unequipClipName;
        [SerializeField] private string _equipClipName;


        private EquipType _equipType;
        private int _index;


        public event Action<int, EquipType, PointerEventData, IEquipmentSlotView> BeginDragEvent;
        public event Action<PointerEventData, IEquipmentSlotView> EndDragEvent;
        public event Action<int, EquipType, PointerEventData, IEquipmentSlotView> DropEvent;

        public void InitDotween(DoTweenAnimationManager doTweenAnimationManager)
        {
            _imageEquipmentSlot.InitDotween(doTweenAnimationManager);
        }

        public void SetDragState()
        {
            _imageEquipmentSlot.SetDragState();
        }

        public void SetNormalState()
        {
            _imageEquipmentSlot.SetNormalState();
        }

        public void DoPunchScale()
        {
            _imageEquipmentSlot.DoPunchScale();
        }

        public void DoWiggleEffect()
        {
            _imageEquipmentSlot.DoWiggle();
        }

        public void SetHighlightedState()
        {
            _imageEquipmentSlot.Highlight();
        }

        public void PlayUnEquipedSound()
        {
            PlaySound(_unequipClipName);
        }

        public void PlayEquipSound()
        {
            PlaySound(_equipClipName);
        }


        public void SetEquipType(EquipType equipType)
        {
            _equipType = equipType;
        }


        public void SetSprite(Sprite sprite)
        {
            _imageEquipmentSlot.SetSprite(sprite);
        }

        public void SetDefaultSprite()
        {
            _imageEquipmentSlot.SetDefaultSprite();
        }


        public void AddButtonListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
            _button.onClick.AddListener(() => PlaySound(_clickClipName));
        }

        private void PlaySound(string clipName)
        {
            if (AudioManager.Instance.TryGetAudioClipByName(clipName, out var audioClip))
            {
                AudioManager.Instance.PlaySound(audioClip, AudioOutput.UI);
            }
        }

        public void RemoveAllButtonListeners()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void SetIndex(int i)
        {
            _index = i;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDragEvent?.Invoke(_index, _equipType, eventData, this);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDragEvent?.Invoke(eventData, this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            DropEvent?.Invoke(_index, _equipType, eventData, this);
        }

        private void OnDestroy()
        {
            _imageEquipmentSlot.KillTween();
        }
    }
}
