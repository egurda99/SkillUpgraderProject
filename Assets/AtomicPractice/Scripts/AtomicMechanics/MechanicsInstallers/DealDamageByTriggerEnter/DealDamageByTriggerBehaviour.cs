using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class DealDamageByTriggerBehaviour : IEntityInit, IEntityDispose
{
    private TriggerEventDispatcher _triggerEventDispatcher;
    private ReactiveVariable<float> _damage;
    private Transform _root;


    public void Init(IEntity entity)
    {
        _triggerEventDispatcher = entity.GetTriggerEventDispatcher();
        _damage = entity.GetAttackDamage();
        _root = entity.GetRootTransform();

        _triggerEventDispatcher.OnTriggerEntered += OnTriggerEntered;
    }

    private void OnTriggerEntered(Collider other)
    {
        if (other.TryGetComponent(out SceneEntityProxy entity))
        {
            entity.GetTakeDamageAction().Invoke(_damage.Value);
            Object.Destroy(_root.gameObject);
        }
    }

    public void Dispose(IEntity entity)
    {
        _triggerEventDispatcher.OnTriggerEntered -= OnTriggerEntered;
    }
}
