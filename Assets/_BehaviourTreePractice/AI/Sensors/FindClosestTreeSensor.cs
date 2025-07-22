using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviourTreePractice;
using UnityEngine;
using Zenject;
using Tree = BehaviourTreePractice.Tree;

namespace _BehaviourTreePractice
{
    public sealed class FindClosestTreeSensor : MonoBehaviour
    {
        [SerializeField] private BehaviorTree _behaviorTree;
        private Tree _currentTarget;
        private ActiveTreesProvider _activeTreesProvider;

        private const string BlackboardKey = "targetTree";

        [Inject]
        public void Construct(ActiveTreesProvider treeProvider)
        {
            _activeTreesProvider = treeProvider;
        }

        private void Start()
        {
            TryFindTree();
        }


        private void OnEnable()
        {
            _activeTreesProvider.ActiveTreesChanged += OnActiveTreesChanged;
        }

        private void OnDisable()
        {
            _activeTreesProvider.ActiveTreesChanged -= OnActiveTreesChanged;

            if (_currentTarget != null)
            {
                _currentTarget.OnTreeDespawned -= OnTreeDespawned;
                _currentTarget.OnTreeOccupiedStatusChanged -= OnTreeOccupiedStatusChanged;
                _currentTarget.Release(); // ?
            }
        }


        private void OnActiveTreesChanged(IReadOnlyList<Tree> trees)
        {
            TryFindTree();
        }

        private void OnTreeDespawned(Tree tree)
        {
            if (_currentTarget == tree)
            {
                _currentTarget.OnTreeDespawned -= OnTreeDespawned;
                _currentTarget.OnTreeOccupiedStatusChanged -= OnTreeOccupiedStatusChanged;
                _currentTarget = null;
                _behaviorTree.SetVariableValue(BlackboardKey, null);
                TryFindTree();
            }
        }

        private void OnTreeOccupiedStatusChanged(bool value)
        {
            if (value)
            {
                _currentTarget.OnTreeDespawned -= OnTreeDespawned;
                _currentTarget.OnTreeOccupiedStatusChanged -= OnTreeOccupiedStatusChanged;
                _currentTarget.Release();
                _currentTarget = null;
                _behaviorTree.SetVariableValue(BlackboardKey, null);
                TryFindTree();
            }
        }

        private void TryFindTree()
        {
            var trees = _activeTreesProvider.Trees;
            Tree closest = null;
            var minSqrDistance = float.MaxValue;
            var currentPos = transform.position;

            foreach (var tree in trees)
            {
                if (tree.IsTreeOccupied)
                    continue;

                var sqrDist = (tree.transform.position - currentPos).sqrMagnitude;
                if (sqrDist < minSqrDistance)
                {
                    closest = tree;
                    minSqrDistance = sqrDist;
                }
            }

            if (_currentTarget != closest)
            {
                if (_currentTarget != null)
                {
                    _currentTarget.OnTreeDespawned -= OnTreeDespawned;
                    _currentTarget.OnTreeOccupiedStatusChanged -= OnTreeOccupiedStatusChanged;
                    _currentTarget.Release();
                }

                _currentTarget = closest;

                if (_currentTarget != null)
                {
                    _currentTarget.Reserve();
                    _currentTarget.OnTreeDespawned += OnTreeDespawned;
                    _currentTarget.OnTreeOccupiedStatusChanged += OnTreeOccupiedStatusChanged;
                }

                _behaviorTree.SetVariableValue(BlackboardKey, _currentTarget?.transform);
            }
        }
    }
}
