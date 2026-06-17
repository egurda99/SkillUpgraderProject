using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Modules.Popups
{
    public sealed class PopupManagerZenject : MonoBehaviour
    {
        public event Action<PopupPresenter> OnShow;
        public event Action<PopupPresenter> OnHide;

        [SerializeField] private Transform _pool;
        [SerializeField] private Transform _viewport;

        private readonly Dictionary<Type, PopupPresenter> _cache = new();
        private PopupPresenter _current;

        private IInstantiator _instantiator;
        private PopupCatalog _catalog;

        [Inject]
        public void Construct(IInstantiator instantiator, PopupCatalog catalog)
        {
            _instantiator = instantiator;
            _catalog = catalog;
        }

        private void Awake()
        {
            _pool.gameObject.SetActive(false);
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

        public void Show<T>(IPopupArgs args = null) where T : PopupPresenter
        {
            if (_current is T same)
            {
                same.Show(args);
                return;
            }

            Hide();

            T popup = GetOrCreate<T>();
            popup.transform.SetParent(_viewport, false);
            popup.gameObject.SetActive(true);
            popup.Show(args);

            _current = popup;
            OnShow?.Invoke(_current);
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

            T popup = GetOrCreate<T>();
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

            PopupPresenter popup = _current;
            _current = null;
            OnHide?.Invoke(popup);

            popup.Hide(() => popup.transform.SetParent(_pool, false));
        }

        public bool IsShown<T>() where T : PopupPresenter => _current is T;

        private PopupPresenter GetOrCreate(PopupInfo info)
        {
            Type type = info.Prefab.GetType();

            if (_cache.TryGetValue(type, out PopupPresenter existing))
                return existing;

            PopupPresenter popup = _instantiator.InstantiatePrefabForComponent<PopupPresenter>(info.Prefab, _pool);
            popup.gameObject.SetActive(false);

            if (info.Cached)
                _cache[type] = popup;

            return popup;
        }

        private T GetOrCreate<T>() where T : PopupPresenter
        {
            Type type = typeof(T);

            if (_cache.TryGetValue(type, out PopupPresenter existing))
                return (T)existing;

            PopupInfo info = _catalog.GetPopupInfo<T>();

            T popup = _instantiator.InstantiatePrefabForComponent<T>(info.Prefab, _pool);
            popup.gameObject.SetActive(false);

            if (info.Cached)
                _cache[type] = popup;

            return popup;
        }
    }
}
