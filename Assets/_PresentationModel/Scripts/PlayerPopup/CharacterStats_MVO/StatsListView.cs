using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lessons.Architecture.PM
{
    public sealed class StatsListView
    {
        private readonly Dictionary<string, StatViewHolder> _statsDictionary = new();
        private readonly StatViewFactory _statViewFactory;
        private readonly StatAdapterFactory _statAdapterFactory;

        public StatsListView(StatViewFactory statViewFactory, StatAdapterFactory statAdapterFactory)
        {
            _statViewFactory = statViewFactory;
            _statAdapterFactory = statAdapterFactory;
        }

        public void Show()
        {
            foreach (var viewHolder in _statsDictionary.Values)
            {
                viewHolder.Adapter.Show();
            }
        }

        public void Hide()
        {
            foreach (var viewHolder in _statsDictionary.Values)
            {
                viewHolder.Adapter.Hide();
            }
        }


        public void AddStat(CharacterStat stat)
        {
            if (_statsDictionary.ContainsKey(stat.Name))
            {
                Debug.LogWarning("Stat already added");
                return;
            }

            Debug.Log("Added stat" + stat);

            var view = _statViewFactory.GetStatView();
            var adapter = _statAdapterFactory.GetStatAdapter(stat, view);
            var holder = new StatViewHolder(view, adapter);

            _statsDictionary.Add(stat.Name, holder);
        }

        public void RemoveStat(CharacterStat stat)
        {
            if (_statsDictionary.ContainsKey(stat.Name) == false)
            {
                Debug.LogWarning("Stat dont exist");
                return;
            }

            var holder = _statsDictionary[stat.Name];
            holder.Adapter.Dispose();

            Object.Destroy(holder.View.gameObject);


            _statsDictionary.Remove(stat.Name);
        }

        public void Clear()
        {
            foreach (var holder in _statsDictionary.Values)
            {
                holder.Adapter.Dispose();
                Object.Destroy(holder.View.gameObject);
            }

            _statsDictionary.Clear();
        }


        private struct StatViewHolder
        {
            public readonly StatView View;
            public readonly StatAdapter Adapter;

            public StatViewHolder(StatView view, StatAdapter adapter)
            {
                View = view;
                Adapter = adapter;
            }
        }
    }
}
