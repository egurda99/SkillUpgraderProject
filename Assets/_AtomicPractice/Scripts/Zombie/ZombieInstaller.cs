using Atomic.Entities;
using UnityEngine;

public class ZombieInstaller : SceneEntityInstallerBase
{
    [SerializeField] private AutoMoveToTargetMechanic _moveToTargetMechanic;
    [SerializeField] private LifeMechanic _lifeMechanic;

    [SerializeField] private RotateToTargetMechanic _rotateToTargetMechanic;
    [SerializeField] private MeleeReloadMechanic _meleeReloadMechanic;
    [SerializeField] private AutoMeleeAttackMechanic _autoMeleeAttackMechanic;
    [SerializeField] private CheckIfTargetAliveMechanic _checkIfTargetAliveMechanic;
    [SerializeField] private DestroyByLifeTimeMechanic _destroyByLifeTimeMechanic;


    public override void Install(IEntity entity)
    {
        _rotateToTargetMechanic.Install(entity);

        _moveToTargetMechanic.Install(entity);
        _lifeMechanic.Install(entity);
        _meleeReloadMechanic.Install(entity);
        _autoMeleeAttackMechanic.Install(entity);
        _checkIfTargetAliveMechanic.Install(entity);
        _destroyByLifeTimeMechanic.Install(entity);


        entity.GetCanAttack().Append(() => !entity.GetIsDead().Value);
        entity.GetCanAttack().Append(() => !entity.GetNeedReload().Value);
        entity.GetCanAttack().Append(() => entity.GetIsTargetAlive().Value);
        entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
        entity.GetCanMove().Append(() => !entity.GetIsAttacking().Value);
        entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);
        entity.GetCanStartTimer().Append(() => entity.GetIsDead().Value);
    }
}
