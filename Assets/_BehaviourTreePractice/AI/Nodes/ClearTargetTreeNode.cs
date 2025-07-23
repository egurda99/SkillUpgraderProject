using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _BehaviourTreePractice
{
    public sealed class ClearTargetTreeNode : Action
    {
        [SerializeField] private BehaviorTree _blackboard;

        private SharedBool _isFull;
        private SharedTree _tree;

        public override void OnAwake()
        {
            _isFull = (SharedBool)_blackboard.GetVariable(BlackboardKeys.IS_BACKPACK_FULL);
            _tree = (SharedTree)_blackboard.GetVariable(BlackboardKeys.TREE);
        }

        public override TaskStatus OnUpdate()
        {
            if (_isFull.Value == false)
                return TaskStatus.Failure;

            if (_tree.Value != null)
            {
                _tree.Value.Release();
                _tree.Value = null;
            }

            return TaskStatus.Success;
        }
    }
}