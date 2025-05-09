using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class AutoMeleeAttackAfterReloadBehaviour : IEntityInit, IEntityUpdate, IEntityDispose
{
    private bool _reloadEnded;


    private AndExpression _canAttack;

    private IEvent _reloaded;
    private IEvent _attackRequest;
    private IEvent _attackAction;
    private IEvent _attackEvent;
    private EntityTriggerDispatcher _trigerDispatcher;
    private ReactiveVariable<float> _attackDamage;
    private ReactiveVariable<float> _distanceToAttack;
    private ReactiveVariable<Transform> _target;
    private Transform _rootTransform;
    private ReactiveVariable<bool> _isAttacking;

    public void Init(IEntity entity)
    {
        _reloadEnded = false;

        _canAttack = entity.GetCanAttack();
        _distanceToAttack = entity.GetDistanceToAttack();
        _target = entity.GetTarget();
        _rootTransform = entity.GetRootTransform();

        _isAttacking = entity.GetIsAttacking();

        _attackRequest = entity.GetAttackRequest();
        _attackAction = entity.GetAttackAction();
        _attackEvent = entity.GetAttackEvent();
        _attackDamage = entity.GetAttackDamage();

        _trigerDispatcher = entity.GetEntityTriggerDispatcher();


        _reloaded = entity.GetReloaded();

        _trigerDispatcher.OnTriggerEntered += OnTriggerEntered;
        _reloaded.Subscribe(OnReloaded);
    }

    private void OnTriggerEntered(Collider collider)
    {
        if (_canAttack.Value && _reloadEnded && _isAttacking.Value)
        {
            if (collider.TryGetComponent(out SceneEntityProxy proxy))
            {
                var takeDamageEvent = proxy.GetTakeDamageAction();
                takeDamageEvent.Invoke(_attackDamage.Value);
            }

            else if (collider.TryGetComponent(out SceneEntity entity))
            {
                var takeDamageEvent = entity.GetTakeDamageAction();
                takeDamageEvent.Invoke(_attackDamage.Value);
            }
        }
    }

    private void OnReloaded()
    {
        _reloadEnded = true;
    }

    private void OnShoot()
    {
        _reloadEnded = false;
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_distanceToAttack.Value <= (_target.Value.position - _rootTransform.position).magnitude && _canAttack.Value)
        {
            _attackRequest.Invoke();
        }
    }

    public void Dispose(IEntity entity)
    {
        _trigerDispatcher.OnTriggerEntered -= OnTriggerEntered;

        _reloaded.Unsubscribe(OnReloaded);
    }
}