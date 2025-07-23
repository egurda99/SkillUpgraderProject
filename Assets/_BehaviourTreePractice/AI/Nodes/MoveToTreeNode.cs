using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using static _BehaviourTreePractice.BlackboardKeys;

namespace _BehaviourTreePractice
{
    public sealed class MoveToTreeNode : Action
    {
        [SerializeField] private BehaviorTree _blackboard;

        private SharedTree _tree;
        private SharedVector3 _targetPosition;
        private MoveToPositionNode _moveNode;

        public override void OnAwake()
        {
            _tree = (SharedTree)_blackboard.GetVariable(TREE);
            _targetPosition = (SharedVector3)_blackboard.GetVariable(MOVE_POSITION);

            _moveNode = new MoveToPositionNode();
            _moveNode.SetBlackboard(_blackboard);
            _moveNode.OnAwake();
        }

        public override TaskStatus OnUpdate()
        {
            if (_tree == null || _tree.Value == null)
                return TaskStatus.Failure;

            _targetPosition.Value = _tree.Value.Transform.position;
            return _moveNode.OnUpdate();
        }
    }
}
