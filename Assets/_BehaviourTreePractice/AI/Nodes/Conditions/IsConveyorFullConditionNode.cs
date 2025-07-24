using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using static _BehaviourTreePractice.BlackboardKeys;

namespace _BehaviourTreePractice
{
    public sealed class IsConveyorFullConditionNode : Conditional
    {
        private SharedBool _isFull;

        public override void OnAwake()
        {
            _isFull = (SharedBool)GlobalVariables.Instance.GetVariable(IS_CONVEYOR_FULL);
        }

        public override TaskStatus OnUpdate()
        {
            if (_isFull.Value == false)
                return TaskStatus.Failure;

            return TaskStatus.Success;
        }
    }
}
