using Atomic.Elements;
using Atomic.Entities;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using static _BehaviourTreePractice.BlackboardKeys;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace _BehaviourTreePractice
{
    public sealed class MoveToPositionNode : Action
    {
        [SerializeField] private BehaviorTree _blackboard;

        private SharedVector3 _targetPosition;
        private SharedSceneEntity _entity;
        private SharedFloat _stoppingDistance;
        private ReactiveVariable<Vector3> _entityMoveDirection;
        private Vector3 _currentPositon;

        public override void OnAwake()
        {
            _entity = (SharedSceneEntity)_blackboard.GetVariable(UNIT);
            _targetPosition = (SharedVector3)_blackboard.GetVariable(MOVE_POSITION);
            _stoppingDistance = (SharedFloat)_blackboard.GetVariable(STOPPING_DISTANCE);
            _entityMoveDirection = _entity.Value.GetMoveDirection();
            _currentPositon = _entity.Value.GetRootTransform().position;
        }

        public override TaskStatus OnUpdate()
        {
            if (_entity?.Value == null || _targetPosition == null || _stoppingDistance == null)
                return TaskStatus.Failure;


            _currentPositon = _entity.Value.GetRootTransform().position;

            var distance = _targetPosition.Value - _currentPositon;

            if (distance.magnitude <= _stoppingDistance.Value)
            {
                _entityMoveDirection.Value = Vector3.zero;
                return TaskStatus.Success;
            }


            var moveDirection = (_targetPosition.Value - _currentPositon).normalized;

            _entityMoveDirection.Value = moveDirection;

            return TaskStatus.Running;
        }

        public void SetBlackboard(BehaviorTree tree)
        {
            _blackboard = tree;
        }
    }
}
