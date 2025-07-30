using UnityEngine;

namespace MyCodeBase.HybridComponents
{
    public sealed class MoveByTransformSystem
    {
        private readonly MoveData _moveData;
        private readonly Transform _transform;

        public MoveByTransformSystem(MoveData moveData)
        {
            _moveData = moveData;

            _moveData.CanMoveCondition.AddCondition(() => _moveData.CanMove);

            _transform = _moveData.RootTransform;
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_moveData.CanMoveCondition.Invoke())
            {
                _moveData.SetIsMoving(false);
                return;
            }


            var worldDirection = _moveData.Direction;


            if (worldDirection.sqrMagnitude > 0f)
            {
                _moveData.SetIsMoving(true);
                _transform.position += worldDirection * _moveData.Speed * deltaTime;
            }
            else
            {
                _moveData.SetIsMoving(false);
            }
        }
    }
}
