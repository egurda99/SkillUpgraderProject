using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public sealed class HeroListView : MonoBehaviour
    {
        public event Action<HeroView> OnHeroClicked;

        [SerializeField] private List<HeroView> _views;

        private readonly List<HeroViewClickHolder> _clickHolders = new();

        private void OnEnable()
        {
            foreach (var view in _views)
            {
                AddClickHolder(view);
            }
        }

        private void OnDisable()
        {
            foreach (var holder in _clickHolders)
            {
                holder.View.OnClicked -= holder.OnClick;
            }

            _clickHolders.Clear();
        }

        public IReadOnlyList<HeroView> GetViews()
        {
            return _views.AsReadOnly();
        }

        public HeroView GetView(int index)
        {
            return _views[index];
        }

        public void Remove(HeroView view)
        {
            var index = _clickHolders.FindIndex(h => h.View == view);
            if (index >= 0)
            {
                view.OnClicked -= _clickHolders[index].OnClick;
                _clickHolders.RemoveAt(index);
            }

            if (_views.Contains(view))
            {
                _views.Remove(view);
                Destroy(view.gameObject);
            }
        }

        public void Add(HeroView view)
        {
            if (_views.Contains(view))
                return;

            _views.Add(view);
            AddClickHolder(view);
        }

        private void AddClickHolder(HeroView view)
        {
            if (_clickHolders.Exists(h => h.View == view))
                return;

            UnityAction handler = () => OnHeroClicked?.Invoke(view);
            view.OnClicked += handler;
            _clickHolders.Add(new HeroViewClickHolder(view, handler));
        }

        private readonly struct HeroViewClickHolder
        {
            public readonly HeroView View;
            public readonly UnityAction OnClick;

            public HeroViewClickHolder(HeroView view, UnityAction onClick)
            {
                View = view;
                OnClick = onClick;
            }
        }
    }
}
