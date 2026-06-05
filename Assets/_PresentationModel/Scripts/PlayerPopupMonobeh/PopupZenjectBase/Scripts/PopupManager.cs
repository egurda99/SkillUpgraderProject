using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Modules.Popups
{
    public sealed class PopupManager : MonoBehaviour
    {
        public event Action<PopupPresenter> OnShow;
        public event Action<PopupPresenter> OnHide;

        [SerializeField]
        private Transform _pool;

        [SerializeField]
        private Transform _viewport;

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
            popup.Show(args);

            _current = popup;
            OnShow?.Invoke(_current);
        }

        public void Hide()
        {
            if (_current == null)
                return;

            PopupPresenter popup = _current;
            popup.Hide();
            popup.transform.SetParent(_pool, false);

            _current = null;
            OnHide?.Invoke(popup);
        }

        public bool IsShown<T>() where T : PopupPresenter
        {
            return _current is T;
        }

        private T GetOrCreate<T>() where T : PopupPresenter
        {
            Type type = typeof(T);

            if (_cache.TryGetValue(type, out PopupPresenter existing))
                return (T) existing;

            PopupInfo info = _catalog.GetPopupInfo<T>();

            T popup = _instantiator.InstantiatePrefabForComponent<T>(info.Prefab, _pool);
            popup.Manager = this;
            popup.gameObject.SetActive(false);

            if (info.Cached)
                _cache[type] = popup;

            return popup;
        }
    }
}