using Atomic.Entities;
using UnityEngine;

public class CharacterInstaller : SceneEntityInstallerBase
{
    [SerializeField] private MoveToDirectionMechanic _moveToDirectionMechanic;

    [SerializeField] private RotateByMouseMechanic _rotateByMouseMechanic;


    public override void Install(IEntity entity)
    {
        entity.AddPlayerTag();
        _moveToDirectionMechanic.Install(entity);

        _rotateByMouseMechanic.Install(entity);
    }
}
