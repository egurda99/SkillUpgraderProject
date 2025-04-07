using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lessons.Architecture.PM
{
    public sealed class StatsListView
    {
        private readonly Dictionary<CharacterStat, StatViewHolder> _statsDictionary = new();
        private readonly StatViewFactory _statViewFactory;
        private readonly StatAdapterFactory _statAdapterFactory;

        public StatsListView(StatViewFactory statViewFactory, StatAdapterFactory statAdapterFactory)
        {
            _statViewFactory = statViewFactory;
            _statAdapterFactory = statAdapterFactory;
        }


        public void AddStat(CharacterStat stat)
        {
            if (_statsDictionary.ContainsKey(stat))
            {
                Debug.LogWarning("Stat already added");
                return;
            }

            var view = _statViewFactory.GetStatView();
            var adapter = _statAdapterFactory.GetStatAdapter(stat, view);
            var holder = new StatViewHolder(view, adapter);

            _statsDictionary.Add(stat, holder);
        }

        public void RemoveStat(CharacterStat stat)
        {
            if (_statsDictionary.ContainsKey(stat) == false)
            {
                Debug.LogWarning("Stat dont exist");
                return;
            }

            var holder = _statsDictionary[stat];

            Object.Destroy(holder.View.gameObject);

            holder.Adapter.Dispose();

            _statsDictionary.Remove(stat);
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
