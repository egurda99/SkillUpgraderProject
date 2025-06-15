using Atomic.Entities;
using UnityEngine;

public class CharacterVisualInstaller : SceneEntityInstallerBase
{
    [SerializeField] private TwoD_MoveAnimationToDirectionMechanic _twoDMoveAnimationToDirectionMechanic;

    //[SerializeField] private ShootAnimationMechanic _shootAnimationMechanic;
    // [SerializeField] private DieAnimationMechanic _dieAnimationMechanic;
    // [SerializeField] private ShootVFXMechanic _shootVFXMechanic;
    // [SerializeField] private TakeDamageVFXMechanic _takeDamageVFXMechanic;
    //[SerializeField] private StunAnimationMechanic _stunAnimationMechanic;
    // [SerializeField] private StunVFXMechanic _stunVFXMechanic;


    public override void Install(IEntity entity)
    {
        _twoDMoveAnimationToDirectionMechanic.Install(entity);
        // _shootAnimationMechanic.Install(entity);
        // _dieAnimationMechanic.Install(entity);
        // _shootVFXMechanic.Install(entity);
        // _takeDamageVFXMechanic.Install(entity);
        // _stunAnimationMechanic.Install(entity);
        // _stunVFXMechanic.Install(entity);
    }
}
