using Atomic.Entities;
using UnityEngine;

public class BulletInstaller : SceneEntityInstallerBase
{
    [SerializeField] private float _damage = 1f;

    [SerializeField] private MoveToDirectionMechanic _moveToDirectionMechanic;
    [SerializeField] private DestroyByLifeTimeMechanic _destroyByLifeTimeMechanic;


    public override void Install(IEntity entity)
    {
        _moveToDirectionMechanic.Install(entity);
        _destroyByLifeTimeMechanic.Install(entity);

        entity.GetCanStartTimer().Append(() => true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SceneEntityProxy entity))
        {
            entity.GetTakeDamageAction().Invoke(_damage);
            Destroy(gameObject);
        }
    }
}
