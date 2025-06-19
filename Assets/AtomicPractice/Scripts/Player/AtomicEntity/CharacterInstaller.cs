using Atomic.Entities;
using UnityEngine;

public class CharacterInstaller : SceneEntityInstallerBase
{
    [SerializeField] private MoveToDirectionMechanic _moveToDirectionMechanic;
    [SerializeField] private RotateByMouseMechanic _rotateByMouseMechanic;
    [SerializeField] private ShootForwardMechanic _shootForwardMechanic;
    [SerializeField] private LifeMechanic _lifeMechanic;
    [SerializeField] private AmmoMechanic _ammoMechanic;
    [SerializeField] private AmmoRefillMechanic _ammoRefillMechanic;
    [SerializeField] private ShootReloadMechanic _shootReloadMechanic;
    [SerializeField] private StunMechanic _stunMechanic;


    public override void Install(IEntity entity)
    {
        _moveToDirectionMechanic.Install(entity);
        _lifeMechanic.Install(entity);
        _shootForwardMechanic.Install(entity);
        _rotateByMouseMechanic.Install(entity);
        _ammoMechanic.Install(entity);
        _ammoRefillMechanic.Install(entity);
        _shootReloadMechanic.Install(entity);
        _stunMechanic.Install(entity);


        entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
        entity.GetCanMove().Append(() => !entity.GetIsStunned().Value);
        entity.GetCanRefill().Append(() => !entity.GetIsDead().Value);
        entity.GetCanShoot().Append(() => !entity.GetIsDead().Value);
        entity.GetCanShoot().Append(() => !entity.GetIsAmmoEmpty().Value);
        entity.GetCanShoot().Append(() => !entity.GetNeedReload().Value);
        entity.GetCanShoot().Append(() => !entity.GetIsStunned().Value);
        entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);
        entity.GetCanRotate().Append(() => !entity.GetIsStunned().Value);
    }
}
