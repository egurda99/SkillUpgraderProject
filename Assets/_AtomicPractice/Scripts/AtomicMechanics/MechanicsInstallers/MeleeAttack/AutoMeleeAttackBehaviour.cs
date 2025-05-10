using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class AutoMeleeAttackBehaviour : IEntityInit, IEntityUpdate, IEntityDispose
{
    private bool _reloadEnded;


    private AndExpression _canAttack;

    private IEvent _attackRequest;
    private IEvent _attackAction;

    private ReactiveVariable<float> _attackDamage;
    private ReactiveVariable<float> _distanceToAttack;
    private ReactiveVariable<Transform> _target;
    private Transform _rootTransform;
    private ReactiveVariable<bool> _needReload;
    private IEvent _attackEvent;

    public void Init(IEntity entity)
    {
        _canAttack = entity.GetCanAttack();
        _distanceToAttack = entity.GetDistanceToAttack();
        _target = entity.GetTarget();
        _rootTransform = entity.GetRootTransform();

        _needReload = entity.GetNeedReload();

        _attackRequest = entity.GetAttackRequest();
        _attackAction = entity.GetAttackAction();
        _attackEvent = entity.GetAttackEvent();
        _attackDamage = entity.GetAttackDamage();


        _attackAction.Subscribe(OnAttackAction);
    }

    private void OnAttackAction()
    {
        if (_distanceToAttack.Value >= (_target.Value.position - _rootTransform.position).magnitude)
        {
            if (_target.Value.TryGetComponent(out SceneEntityProxy proxy))
            {
                var takeDamageEvent = proxy.GetTakeDamageAction();
                takeDamageEvent.Invoke(_attackDamage.Value);
            }

            else if (_target.Value.TryGetComponent(out SceneEntity targetEntity))
            {
                var takeDamageEvent = targetEntity.GetTakeDamageAction();
                takeDamageEvent.Invoke(_attackDamage.Value);
            }

            _attackEvent?.Invoke();
            _needReload.Value = true;
        }
    }


    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_distanceToAttack.Value >= (_target.Value.position - _rootTransform.position).magnitude && _canAttack.Value)
        {
            _attackRequest.Invoke();
            _needReload.Value = true;
        }
    }

    public void Dispose(IEntity entity)
    {
        _attackAction.Unsubscribe(OnAttackAction);
    }
}
