using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using static _BehaviourTreePractice.BlackboardKeys;
using Tree = BehaviourTreePractice.Tree;

namespace _BehaviourTreePractice
{
    public sealed class MineTreeNode : Action
    {
        [SerializeField] private BehaviorTree _blackboard;
        private SharedTree _tree;
        private SharedBool _isBackpackFull;
        private Tree _minedTree;


        public override void OnAwake()
        {
            _tree = (SharedTree)_blackboard.GetVariable(TREE);
            _isBackpackFull = (SharedBool)_blackboard.GetVariable(IS_BACKPACK_FULL);
        }

        public override void OnStart()
        {
            _minedTree = _tree.Value;
        }

        public override TaskStatus OnUpdate()
        {
            if (_minedTree == null || _isBackpackFull.Value || _tree.Value != _minedTree)
                return TaskStatus.Success;


            return TaskStatus.Running;
        }


        public override void OnEnd()
        {
            _minedTree = null;
        }
    }
}
