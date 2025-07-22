using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _BehaviourTreePractice
{
    public sealed class SetWaypointNode : Action
    {
        [SerializeField] private BehaviorTree _blackboard;

        private SharedTransformList _patrolPoints;
        private SharedInt _waypointIndex;

        public override void OnAwake()
        {
            _patrolPoints = (SharedTransformList)_blackboard.GetVariable(BlackboardKeys.WAYPOINTS);
            _waypointIndex = (SharedInt)_blackboard.GetVariable(BlackboardKeys.WAYPOINT_INDEX);
        }

        public override TaskStatus OnUpdate()
        {
            if (_patrolPoints.Value == null)
                return TaskStatus.Failure;

            var targetPosition = _patrolPoints.Value[_waypointIndex.Value].position;

            _blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, (SharedVector3)targetPosition);
            return TaskStatus.Success;
        }
    }
}
