using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using static _BehaviourTreePractice.BlackboardKeys;

namespace _BehaviourTreePractice
{
    public sealed class HasTreeNode : Action
    {
        [SerializeField] private BehaviorTree _blackboard;

        private SharedTree _treeTarget;

        public override void OnAwake()
        {
            _treeTarget = (SharedTree)_blackboard.GetVariable(TREE);
        }

        public override TaskStatus OnUpdate()
        {
            if (_treeTarget.Value == null)
                return TaskStatus.Failure;

            return TaskStatus.Success;
        }
    }
}
