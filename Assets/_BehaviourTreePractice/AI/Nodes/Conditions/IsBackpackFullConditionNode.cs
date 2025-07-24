using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using static _BehaviourTreePractice.BlackboardKeys;

namespace _BehaviourTreePractice
{
    public sealed class IsBackpackFullConditionNode : Conditional
    {
        [SerializeField] private BehaviorTree _blackboard;
        private SharedBool _isFull;

        public override void OnAwake()
        {
            _isFull = (SharedBool)_blackboard.GetVariable(IS_BACKPACK_FULL);
        }

        public override TaskStatus OnUpdate()
        {
            return _isFull.Value ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
