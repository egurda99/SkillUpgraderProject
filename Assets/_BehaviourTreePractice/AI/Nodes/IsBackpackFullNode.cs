using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _BehaviourTreePractice
{
    public sealed class IsBackpackFullNode : Action
    {
        [SerializeField] private BehaviorTree _blackboard;

        private SharedBool _isFull;

        public override void OnAwake()
        {
            _isFull = (SharedBool)_blackboard.GetVariable(BlackboardKeys.IS_BACKPACK_FULL);
        }

        public override TaskStatus OnUpdate()
        {
            if (_isFull.Value == false)
                return TaskStatus.Failure;

            return TaskStatus.Success;
        }
    }
}