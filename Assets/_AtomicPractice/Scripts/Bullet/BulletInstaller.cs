using Atomic.Entities;
using UnityEngine;

public class BulletInstaller : SceneEntityInstallerBase
{
    [SerializeField] private MoveToDirectionMechanic _moveToDirectionMechanic;
    [SerializeField] private DestroyByLifeTimeMechanic _destroyByLifeTimeMechanic;
    [SerializeField] private DealDamageByTriggerMechanic _dealDamageByTriggerMechanic;


    public override void Install(IEntity entity)
    {
        _moveToDirectionMechanic.Install(entity);
        _destroyByLifeTimeMechanic.Install(entity);
        _dealDamageByTriggerMechanic.Install(entity);

        entity.GetCanStartTimer().Append(() => true);
    }
}
