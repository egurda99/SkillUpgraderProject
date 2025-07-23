using System;
using System.Collections.Generic;
using Atomic.Elements;
using BehaviourTreePractice;
using UnityEngine;
using Zenject;
using Tree = BehaviourTreePractice.Tree;

namespace _BehaviourTreePractice
{
    public sealed class FindClosestTreeSensor : MonoBehaviour
    {
        private Tree _currentTarget;
        private ActiveTreesProvider _activeTreesProvider;
        private ReactiveVariable<string> _id;

        public event Action OnSensorEnabled;
        public event Action OnSensorDisabled;
        public event Action OnSensorDestroyed;
        public event Action<Tree> OnTreeFound;
        public event Action<Tree> OnTreeRemoved;

        [Inject]
        public void Construct(ActiveTreesProvider treeProvider)
        {
            _activeTreesProvider = treeProvider;
        }

        public void Init(ReactiveVariable<string> id)
        {
            _id = id;
        }

        private void Start()
        {
            TryFindTree();
        }

        private void OnEnable()
        {
            OnSensorEnabled?.Invoke();
            _activeTreesProvider.ActiveTreesChanged += OnActiveTreesChanged;
        }

        private void OnDisable()
        {
            _activeTreesProvider.ActiveTreesChanged -= OnActiveTreesChanged;
            OnSensorDisabled?.Invoke();

            if (_currentTarget != null)
            {
                _currentTarget.OnTreeDespawned -= OnTreeDespawned;
                _currentTarget.OnTreeOccupiedStatusChanged -= OnTreeOccupiedStatusChanged;
                _currentTarget = null;

                OnTreeRemoved?.Invoke(null);
            }
        }

        private void OnDestroy()
        {
            OnSensorDestroyed?.Invoke();
        }

        private void OnActiveTreesChanged(IReadOnlyList<Tree> trees)
        {
            TryFindTree();
        }

        private void OnTreeDespawned(Tree tree)
        {
            if (_currentTarget != tree)
                return;

            _currentTarget.OnTreeDespawned -= OnTreeDespawned;
            _currentTarget.OnTreeOccupiedStatusChanged -= OnTreeOccupiedStatusChanged;
            _currentTarget = null;

            OnTreeRemoved?.Invoke(null);

            TryFindTree();
        }

        private void OnTreeOccupiedStatusChanged(bool isOccupied)
        {
            // если дерево стало занято — пробуем найти другое
            if (isOccupied && _currentTarget != null)
            {
                _currentTarget.OnTreeDespawned -= OnTreeDespawned;
                _currentTarget.OnTreeOccupiedStatusChanged -= OnTreeOccupiedStatusChanged;
                _currentTarget = null;

                OnTreeRemoved?.Invoke(null);

                TryFindTree();
            }
        }

        private void TryFindTree()
        {
            if (_currentTarget != null && _currentTarget.TryReserve(_id.Value))
            {
                return;
            }

            if (_currentTarget != null)
            {
                _currentTarget.OnTreeDespawned -= OnTreeDespawned;
                _currentTarget.OnTreeOccupiedStatusChanged -= OnTreeOccupiedStatusChanged;
                _currentTarget.Release();
                _currentTarget = null;
                OnTreeRemoved?.Invoke(null);
            }

            var trees = _activeTreesProvider.Trees;
            Tree closest = null;
            var minSqrDistance = float.MaxValue;

            foreach (var tree in trees)
            {
                if (tree.IsTreeOccupied)
                    continue;

                if (!tree.TryReserve(_id.Value))
                    continue;

                var dist = (tree.transform.position - transform.position).sqrMagnitude;
                if (dist < minSqrDistance)
                {
                    if (closest != null)
                        closest.Release();

                    closest = tree;
                    minSqrDistance = dist;
                }
                else
                {
                    tree.Release();
                }
            }

            _currentTarget = closest;

            if (_currentTarget != null)
            {
                _currentTarget.OnTreeDespawned += OnTreeDespawned;
                _currentTarget.OnTreeOccupiedStatusChanged += OnTreeOccupiedStatusChanged;

                OnTreeFound?.Invoke(_currentTarget);
            }
            else
            {
                OnTreeRemoved?.Invoke(null);
            }
        }
    }
}
