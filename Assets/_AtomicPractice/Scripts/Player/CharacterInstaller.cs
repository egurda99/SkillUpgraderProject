using Atomic.Entities;
using UnityEngine;

public class CharacterInstaller : SceneEntityInstallerBase
{
    [SerializeField] private MoveByTransformMechanic _moveByTransformMechanic;
    [SerializeField] private RotateByMouseMechanic _rotateByMouseMechanic;
    [SerializeField] private ShootForwardMechanic _shootForwardMechanic;
    [SerializeField] private LifeMechanic _lifeMechanic;
    [SerializeField] private AmmoMechanic _ammoMechanic;
    [SerializeField] private AmmoRefillMechanic _ammoRefillMechanic;
    [SerializeField] private ReloadMechanic _reloadMechanic;


    public override void Install(IEntity entity)
    {
        _moveByTransformMechanic.Install(entity);
        _lifeMechanic.Install(entity);
        _shootForwardMechanic.Install(entity);
        _rotateByMouseMechanic.Install(entity);
        _ammoMechanic.Install(entity);
        _ammoRefillMechanic.Install(entity);
        _reloadMechanic.Install(entity);


        entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
        entity.GetCanShoot().Append(() => !entity.GetIsDead().Value);
        entity.GetCanShoot().Append(() => !entity.GetIsAmmoEmpty().Value);
        entity.GetCanShoot().Append(() => entity.GetReloadEnded().Value);
        entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);
    }
}
