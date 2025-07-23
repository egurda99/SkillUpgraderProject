using Atomic.Entities;
using BehaviourTreePractice;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _BehaviourTreePractice
{
    public class WorkerInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private MoveToDirectionMechanic _moveToDirectionMechanic;

        [SerializeField] private RotateToMoveDirectionMechanic _rotateToMoveDirectionMechanic;

        [SerializeField] [ReadOnly] private string _id;

        public override void Install(IEntity entity)
        {
            entity.AddPlayerTag();
            _moveToDirectionMechanic.Install(entity);

            _rotateToMoveDirectionMechanic.Install(entity);

            var instanceId = GetInstanceID().ToString();
            _id = IdGenerator.Generate<SceneEntity>("Entity_") + "_" + instanceId;

            entity.AddEntityID(_id);
        }
    }
}
