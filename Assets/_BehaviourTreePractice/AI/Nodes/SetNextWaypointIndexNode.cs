using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using static _BehaviourTreePractice.BlackboardKeys;

namespace _BehaviourTreePractice
{
    public sealed class SetNextWaypointIndexNode : Action
    {
        [SerializeField] private BehaviorTree _blackboard;

        private SharedTransformList _patrolPoints;
        private SharedInt _waypointIndex;

        public override void OnAwake()
        {
            _patrolPoints = (SharedTransformList)_blackboard.GetVariable(WAYPOINTS);
            _waypointIndex = (SharedInt)_blackboard.GetVariable(WAYPOINT_INDEX);
        }

        public override TaskStatus OnUpdate()
        {
            if (_patrolPoints.Value == null)
                return TaskStatus.Failure;

            _waypointIndex.Value++;
            _waypointIndex.Value %= _patrolPoints.Value.Count;

            _blackboard.SetVariable(WAYPOINT_INDEX, _waypointIndex);
            return TaskStatus.Success;
        }
    }
}
