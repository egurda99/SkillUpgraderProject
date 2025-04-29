using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PopupManager : MonoBehaviour
    {
        [SerializeField] private PopupHolder[] _allPopups;
        private readonly Dictionary<PopupName, Popup> _activePopups = new();

        private void Awake()
        {
            foreach (var popupHolder in _allPopups)
            {
                var popup = popupHolder.Popup.GetComponent<Popup>();

                if (popup == null)
                {
                    throw new ArgumentException("Popup component is missing a popup component");
                }

                popup.OnPopupCloseRequested += OnPopupCloseRequested;

                popupHolder.Popup.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            foreach (var popupHolder in _allPopups)
            {
                var popup = popupHolder.Popup.GetComponent<Popup>();

                if (popup != null)
                {
                    popup.OnPopupCloseRequested -= OnPopupCloseRequested;
                }
            }
        }

        [Title("Methods")]
        [Button]
        public void ShowPopup(PopupName name)
        {
            if (IsPopupActive(name))
            {
                return;
            }

            var popup = FindPopup(name);
            popup.gameObject.SetActive(true);
            popup.Show();
            _activePopups.Add(name, popup);
        }

        [Button]
        public void HidePopup(PopupName name)
        {
            if (!IsPopupActive(name))
            {
                return;
            }

            var popup = _activePopups[name];
            popup.Hide();
            popup.gameObject.SetActive(false);
            _activePopups.Remove(name);
        }

        [Button]
        public bool IsPopupActive(PopupName name)
        {
            return _activePopups.ContainsKey(name);
        }

        public PopupName FindName(Popup popup)
        {
            foreach (var holder in _allPopups)
            {
                if (ReferenceEquals(holder.Popup, popup))
                {
                    return holder.Name;
                }
            }

            throw new Exception($"Name of popup {popup.name} is not found!");
        }

        private Popup FindPopup(PopupName name)
        {
            foreach (var holder in _allPopups)
            {
                if (holder.Name == name)
                {
                    return holder.Popup;
                }
            }

            throw new Exception($"Popup with name {name} is not found!");
        }


        private void OnPopupCloseRequested(Popup popup)
        {
            HidePopup(FindName(popup));
        }


        [Serializable]
        private struct PopupHolder
        {
            [SerializeField] public PopupName Name;

            [SerializeField] public Popup Popup;
        }
    }
}
