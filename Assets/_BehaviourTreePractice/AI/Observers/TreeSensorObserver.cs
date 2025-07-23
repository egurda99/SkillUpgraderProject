using System;
using BehaviorDesigner.Runtime;
using BehaviourTreePractice;
using Zenject;
using static _BehaviourTreePractice.BlackboardKeys;

namespace _BehaviourTreePractice
{
    public sealed class TreeSensorObserver : IInitializable, IDisposable
    {
        private readonly FindClosestTreeSensor _treeSensor;
        private readonly BehaviorTree _behaviorTree;
        private readonly SharedTree _treeTarget;
        private bool _isInitialized;

        public TreeSensorObserver(FindClosestTreeSensor treeSensor, BehaviorTree behaviorTree)
        {
            _treeSensor = treeSensor;
            _behaviorTree = behaviorTree;
            _treeTarget = (SharedTree)_behaviorTree.GetVariable(TREE);


            _treeSensor.OnSensorEnabled += Initialize;
            _treeSensor.OnSensorDisabled += OnDisable;
            _treeSensor.OnSensorDestroyed += Dispose;
        }

        public void Initialize()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;

            _treeSensor.OnTreeFound += OnTreeFound;
            _treeSensor.OnTreeRemoved += OnTreeRemoved;
        }

        private void OnDisable()
        {
            if (!_isInitialized)
                return;
            _isInitialized = false;

            _treeSensor.OnTreeFound -= OnTreeFound;
            _treeSensor.OnTreeRemoved -= OnTreeRemoved;
        }

        private void OnTreeFound(Tree tree)
        {
            _treeTarget.Value = tree;
        }

        private void OnTreeRemoved(Tree tree)
        {
            if (_treeTarget.Value == tree)
            {
                _treeTarget.Value = null;
            }
        }

        public void Dispose()
        {
            _treeSensor.OnSensorEnabled -= Initialize;
            _treeSensor.OnSensorDisabled -= OnDisable;
            _treeSensor.OnSensorDestroyed -= Dispose;

            _treeSensor.OnTreeFound -= OnTreeFound;
            _treeSensor.OnTreeRemoved -= OnTreeRemoved;
        }
    }
}
