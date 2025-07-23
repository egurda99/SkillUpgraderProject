using System;
using System.Collections.Generic;
using BehaviourTreePractice;
using MyTimer;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Tree = BehaviourTreePractice.Tree;

namespace _BehaviourTreePractice
{
    public sealed class FindClosestTreeSensor : MonoBehaviour
    {
        [SerializeField] private float _duration = 2f;

        private ActiveTreesProvider _activeTreesProvider;
        [ShowInInspector] [ReadOnly] private Timer _timer;

        public event Action<Tree> OnTreeSpotted;

        [Inject]
        public void Construct(ActiveTreesProvider treeProvider, Timer timer)
        {
            _activeTreesProvider = treeProvider;
            _timer = timer;
        }

        private void Start()
        {
            _timer.SetInterval(_duration);
            _timer.Start();
        }

        private void Update()
        {
            _timer.Tick();
        }

        private void OnEnable()
        {
            _activeTreesProvider.ActiveTreesChanged += OnActiveActiveTreesesChanged;
            TryFindTree();
            _timer.OnElapsed += OnTimerElapsed;
        }

        private void OnTimerElapsed()
        {
            _timer.Reset();
            _timer.Start();
            TryFindTree();
        }

        private void OnDisable()
        {
            _activeTreesProvider.ActiveTreesChanged -= OnActiveActiveTreesesChanged;
            _timer.OnElapsed -= OnTimerElapsed;
        }

        private void OnActiveActiveTreesesChanged(IReadOnlyList<Tree> _)
        {
            TryFindTree();
        }

        public void TryFindTree()
        {
            var trees = _activeTreesProvider.Trees;
            Tree closest = null;
            var minSqrDistance = float.MaxValue;

            foreach (var tree in trees)
            {
                if (tree.IsTreeOccupied)
                    continue;

                var dist = (tree.transform.position - transform.position).sqrMagnitude;
                if (dist < minSqrDistance)
                {
                    closest = tree;
                    minSqrDistance = dist;
                }
            }

            if (closest != null)
            {
                OnTreeSpotted?.Invoke(closest);
            }
        }
    }
}
