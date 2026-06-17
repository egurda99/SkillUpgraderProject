using System;
using System.Collections.Generic;
using Modules.Popups;
using UnityEngine;

namespace Modules.PopupsStandalone
{
    public sealed class PopupManagerNoZenject : MonoBehaviour
    {
        public event Action<PopupPresenter> OnShow;
        public event Action<PopupPresenter> OnHide;

        [SerializeField] private Transform _pool;

        [SerializeField] private Transform _viewport;

        [SerializeField] private PopupCatalog _catalog;

        private readonly Dictionary<Type, PopupPresenter> _cache = new();

        private PopupPresenter _current;

        private void Awake()
        {
            _pool.gameObject.SetActive(false);
        }

        public void Show<T, A>(A args = default)
            where A : struct, IPopupArgs
            where T : PopupPresenter<A>
        {
            if (_current is T same)
            {
                same.Show(args);
                return;
            }

            Hide();

            var popup = GetOrCreate<T>();
            popup.transform.SetParent(_viewport, false);
            popup.gameObject.SetActive(true);
            popup.Show(args);

            _current = popup;
            OnShow?.Invoke(_current);
        }

        public void Show<T>(IPopupArgs args = null) where T : PopupPresenter
        {
            if (_current is T same)
            {
                same.Show(args);
                return;
            }

            Hide();

            var popup = GetOrCreate<T>();
            popup.transform.SetParent(_viewport, false);
            popup.gameObject.SetActive(true);
            popup.Show(args);

            _current = popup;
            OnShow?.Invoke(_current);
        }

        public void Hide()
        {
            if (_current == null)
                return;

            var popup = _current;
            _current = null;
            OnHide?.Invoke(popup);

            popup.Hide(() => popup.transform.SetParent(_pool, false));
        }

        public void Show(PopupType type, IPopupArgs args = null)
        {
            PopupInfo info = _catalog.GetPopupInfo(type);

            if (_current != null && _current.GetType() == info.Prefab.GetType())
            {
                _current.Show(args);
                return;
            }

            Hide();

            PopupPresenter popup = GetOrCreate(info);
            popup.transform.SetParent(_viewport, false);
            popup.gameObject.SetActive(true);
            popup.Show(args);

            _current = popup;
            OnShow?.Invoke(_current);
        }

        public bool IsShown<T>() where T : PopupPresenter
        {
            return _current is T;
        }

        private PopupPresenter GetOrCreate(PopupInfo info)
        {
            Type type = info.Prefab.GetType();

            if (_cache.TryGetValue(type, out PopupPresenter existing))
                return existing;

            PopupPresenter popup = Instantiate(info.Prefab, _pool);
            popup.gameObject.SetActive(false);

            if (info.Cached)
                _cache[type] = popup;

            return popup;
        }

        private T GetOrCreate<T>() where T : PopupPresenter
        {
            var type = typeof(T);

            if (_cache.TryGetValue(type, out var existing))
                return (T)existing;

            var info = _catalog.GetPopupInfo<T>();

            var popup = (T)Instantiate(info.Prefab, _pool);
            popup.gameObject.SetActive(false);

            if (info.Cached)
                _cache[type] = popup;

            return popup;
        }
    }
}
