using Atomic.Entities;
using UnityEngine;

public class CharacterVisualInstaller : SceneEntityInstallerBase
{
    //[SerializeField] private OneD_MoveAnimationMechanic _oneDMoveAnimationMechanic;
    [SerializeField] private TwoD_MoveAnimationMechanic _twoDMoveAnimationMechanic;

    [SerializeField] private ShootAnimationMechanic _shootAnimationMechanic;
    [SerializeField] private DieAnimationMechanic _dieAnimationMechanic;


    public override void Install(IEntity entity)
    {
        // _oneDMoveAnimationMechanic.Install(entity);
        _twoDMoveAnimationMechanic.Install(entity);
        _shootAnimationMechanic.Install(entity);
        _dieAnimationMechanic.Install(entity);
    }
}
