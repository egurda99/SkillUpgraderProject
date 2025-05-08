using Atomic.Entities;
using UnityEngine;

public class CharacterInstaller : SceneEntityInstallerBase
{
    [SerializeField] private MoveByTransformMechanic _moveByTransformMechanic;
    [SerializeField] private ShootForwardMechanic _shootForwardMechanic;
    [SerializeField] private LifeMechanic _lifeMechanic;


    public override void Install(IEntity entity)
    {
        _moveByTransformMechanic.Install(entity);
        _lifeMechanic.Install(entity);
        _shootForwardMechanic.Install(entity);

        entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
        entity.GetCanShoot().Append(() => !entity.GetIsDead().Value);
    }
}
