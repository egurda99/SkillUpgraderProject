using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using static _BehaviourTreePractice.BlackboardKeys;

namespace _BehaviourTreePractice
{
    public sealed class MoveToConveyorNode : Action
    {
        [SerializeField] private BehaviorTree _blackboard;

        private SharedVector3 _conveyorPosition;
        private SharedVector3 _targetPosition;
        private MoveToPositionNode _moveNode;

        public override void OnAwake()
        {
            _conveyorPosition = (SharedVector3)_blackboard.GetVariable(CONVEYOR_POSITION);
            _targetPosition = (SharedVector3)_blackboard.GetVariable(MOVE_POSITION);

            _moveNode = new MoveToPositionNode();
            _moveNode.SetBlackboard(_blackboard);
            _moveNode.OnAwake();
        }

        public override TaskStatus OnUpdate()
        {
            if (_conveyorPosition == null || _conveyorPosition.Value == null)
                return TaskStatus.Failure;

            _targetPosition.Value = _conveyorPosition.Value;

            return _moveNode.OnUpdate();
        }
    }
}
