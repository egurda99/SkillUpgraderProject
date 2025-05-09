using Atomic.Entities;
using UnityEngine;

public class ZombieInstaller : SceneEntityInstallerBase
{
    [SerializeField] private AutoMoveToTargetMechanic _moveToTargetMechanic;
    [SerializeField] private LifeMechanic _lifeMechanic;

    [SerializeField] private RotateToTargetMechanic _rotateToTargetMechanic;
    // [SerializeField] private ReloadMechanic _reloadMechanic;
    // [SerializeField] private AutoMeleeAttackAfterReloadMechanic _autoMeleeAttackAfterReloadMechanic;


    public override void Install(IEntity entity)
    {
        _rotateToTargetMechanic.Install(entity);

        _moveToTargetMechanic.Install(entity);
        _lifeMechanic.Install(entity);
        //  _reloadMechanic.Install(entity);
        //  _autoMeleeAttackAfterReloadMechanic.Install(entity);
        //  entity.AddShootAction(entity.GetAttackAction());


        //  entity.GetCanAttack().Append(() => !entity.GetIsDead().Value);
        //  entity.GetCanAttack().Append(() => entity.GetReloadEnded().Value);
        entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
        entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);
    }
}
