using System;
using Atomic.Elements;
using BehaviorDesigner.Runtime;
using Zenject;
using static _BehaviourTreePractice.BlackboardKeys;
using Tree = BehaviourTreePractice.Tree;

namespace _BehaviourTreePractice
{
    public sealed class TreeSensorObserver : IInitializable, IDisposable
    {
        private readonly FindClosestTreeSensor _sensor;
        private readonly TreeReservationPolicy _policy;

        private Tree _currentTree;
        private readonly SharedTree _treeTarget;


        public TreeSensorObserver(
            FindClosestTreeSensor sensor,
            BehaviorTree behaviorTree,
            ReactiveVariable<string> id
        )
        {
            _sensor = sensor;
            var backpackFull = (SharedBool)behaviorTree.GetVariable(IS_BACKPACK_FULL);
            _policy = new TreeReservationPolicy(id, backpackFull, this);

            _treeTarget = (SharedTree)behaviorTree.GetVariable(TREE);
        }

        public void Initialize()
        {
            _sensor.OnTreeSpotted += OnTreeSpotted;
        }

        private void OnTreeSpotted(Tree tree)
        {
            if (_policy.TryReserve(tree, _currentTree, out var reservedTree))
            {
                BindToTree(reservedTree);
            }
        }

        private void BindToTree(Tree tree)
        {
            ClearTree();

            _currentTree = tree;
            _treeTarget.Value = tree;

            tree.OnTreeDespawned += OnTreeDespawned;
            tree.OnTreeOccupiedStatusChanged += OnTreeOccupiedStatusChanged;
        }

        private void OnTreeDespawned(Tree _)
        {
            ClearTree();
            _sensor.TryFindTree();
        }

        private void OnTreeOccupiedStatusChanged(bool isOccupied)
        {
            if (_currentTree.IsTreeOccupiedByAnother(_policy.ID.Value))
            {
                ClearTree();
                _sensor.TryFindTree();
            }
        }

        public void ClearTree()
        {
            if (_currentTree == null)
                return;

            _currentTree.OnTreeDespawned -= OnTreeDespawned;
            _currentTree.OnTreeOccupiedStatusChanged -= OnTreeOccupiedStatusChanged;
            _currentTree.Release();

            _currentTree = null;
            _treeTarget.Value = null;
        }

        public void Dispose()
        {
            _sensor.OnTreeSpotted -= OnTreeSpotted;
            ClearTree();
        }
    }
}
