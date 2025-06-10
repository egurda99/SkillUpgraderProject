using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public sealed class HeroListView : MonoBehaviour
    {
        public event Action<HeroView> OnHeroClicked;

        [SerializeField] private HeroView[] views;


        private void OnEnable()
        {
            foreach (var view in views)
            {
                view.OnClicked += () => OnHeroClicked?.Invoke(view);
            }
        }

        private void OnDisable()
        {
            var @event = OnHeroClicked;
            if (@event == null)
            {
                return;
            }

            foreach (var @delegate in @event.GetInvocationList())
            {
                OnHeroClicked -= (Action<HeroView>)@delegate;
            }
        }

        public IReadOnlyList<HeroView> GetViews()
        {
            return views;
        }

        public HeroView GetView(int index)
        {
            return views[index];
        }
    }
}
