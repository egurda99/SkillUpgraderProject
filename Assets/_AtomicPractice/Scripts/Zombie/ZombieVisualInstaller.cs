using Atomic.Entities;
using UnityEngine;

public class ZombieVisualInstaller : SceneEntityInstallerBase
{
    [SerializeField] private TwoD_MoveAnimationToDirectionMechanic _twoDMoveAnimationToDirectionMechanic;

    [SerializeField] private DieAnimationMechanic _dieAnimationMechanic;
    // [SerializeField] private MeleeAttackAnimationMechanic _meleeAttackAnimationMechanic;


    public override void Install(IEntity entity)
    {
        _twoDMoveAnimationToDirectionMechanic.Install(entity);
        _dieAnimationMechanic.Install(entity);
        //_meleeAttackAnimationMechanic.Install(entity);
    }
}
