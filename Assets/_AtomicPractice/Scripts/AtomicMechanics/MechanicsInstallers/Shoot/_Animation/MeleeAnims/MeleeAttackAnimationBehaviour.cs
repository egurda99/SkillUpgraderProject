using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class MeleeAttackAnimationBehaviour : IEntityInit, IEntityDispose
{
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

    private Animator _animator;
    private AnimationEventDispatcher _animationEventDispatcher;
    private IEvent _attackRequsted;
    private IEvent _attackAction;
    private ReactiveVariable<bool> _isAttacking;
    private IEvent _attackEvent;

    public void Init(IEntity entity)
    {
        _animator = entity.GetAnimator();
        _animationEventDispatcher = entity.GetAnimationEventDispatcher();

        _isAttacking = entity.GetIsAttacking();
        _attackRequsted = entity.GetAttackRequest();
        _attackAction = entity.GetAttackAction();
        _attackEvent = entity.GetAttackEvent();


        _attackRequsted.Subscribe(OnAttackRequsted);
        _isAttacking.Subscribe(OnIsAttackingChanged);
        _animationEventDispatcher.OnEventReceived += OnEventReceived;
    }

    private void OnIsAttackingChanged(bool value)
    {
        _animator.SetBool(IsAttacking, _isAttacking.Value);
    }

    private void OnEventReceived(string eventName)
    {
        if (eventName == "Attacked")
        {
            _attackAction.Invoke();
        }

        else if (eventName == "AttackEnded")
        {
            _isAttacking.Value = false;
            _attackEvent?.Invoke();
        }
    }

    private void OnAttackRequsted()
    {
        if (!_isAttacking.Value)
        {
            _isAttacking.Value = true;
        }
    }

    public void Dispose(IEntity entity)
    {
        _attackRequsted.Unsubscribe(OnAttackRequsted);
        _animationEventDispatcher.OnEventReceived -= OnEventReceived;
        _isAttacking.Unsubscribe(OnIsAttackingChanged);
    }
}
