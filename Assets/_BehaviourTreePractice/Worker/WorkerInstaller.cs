using Atomic.Entities;
using UnityEngine;

namespace _BehaviourTreePractice
{
    public class WorkerInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private MoveToDirectionMechanic _moveToDirectionMechanic;

        [SerializeField] private RotateToMoveDirectionMechanic _rotateToMoveDirectionMechanic;


        public override void Install(IEntity entity)
        {
            entity.AddPlayerTag();
            _moveToDirectionMechanic.Install(entity);

            _rotateToMoveDirectionMechanic.Install(entity);
        }
    }
}
