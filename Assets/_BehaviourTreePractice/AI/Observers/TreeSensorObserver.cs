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
        private readonly BehaviorTree _behaviorTree;
        private readonly ReactiveVariable<string> _id;
        private readonly SharedTree _treeTarget;
        private readonly SharedBool _isBackpackFull;

        private Tree _currentTree;

        public TreeSensorObserver(FindClosestTreeSensor sensor, BehaviorTree behaviorTree, ReactiveVariable<string> id)
        {
            _sensor = sensor;
            _behaviorTree = behaviorTree;
            _id = id;
            _treeTarget = (SharedTree)_behaviorTree.GetVariable(TREE);
            _isBackpackFull = (SharedBool)_behaviorTree.GetVariable(IS_BACKPACK_FULL);
        }

        public void Initialize()
        {
            _sensor.OnTreeSpotted += OnTreeSpotted;
        }

        private void OnTreeSpotted(Tree tree)
        {
            if (_isBackpackFull.Value)
                return;


            if (tree.IsTreeOccupied && !tree.IsReservedBy(_id.Value))
                return;

            if (_currentTree != null)
            {
                if (_currentTree.IsReservedBy(_id.Value))
                    return;

                if (_currentTree == tree && _currentTree.IsReservedBy(_id.Value))
                    return;

                if (tree.TryReserve(_id.Value))
                {
                    BindToTree(tree);
                }

                return;
            }

            if (_currentTree == null)
            {
                if (tree.TryReserve(_id.Value))
                {
                    BindToTree(tree);
                }
            }
        }

        private void BindToTree(Tree tree)
        {
            if (_currentTree == tree)
                return;

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
            if (_currentTree == null)
                return;

            if (!isOccupied)
            {
                if (_treeTarget.Value == _currentTree)
                    return;


                _sensor.TryFindTree();
                return;
            }

            if (_currentTree.IsTreeOccupiedByAnother(_id.Value))
            {
                ClearTree();
                _sensor.TryFindTree();
            }
        }

        private void ClearTree()
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
