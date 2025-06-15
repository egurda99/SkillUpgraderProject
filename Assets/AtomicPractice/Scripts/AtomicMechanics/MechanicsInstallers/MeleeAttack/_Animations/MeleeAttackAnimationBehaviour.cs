using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class MeleeAttackAnimationBehaviour : IEntityInit, IEntityDispose
{
    private static readonly int Attack = Animator.StringToHash("Attack");

    private Animator _animator;
    private AnimationEventDispatcher _animationEventDispatcher;
    private IEvent _attackRequsted;
    private IEvent _attackAction;
    private ReactiveVariable<bool> _isAttacking;

    public void Init(IEntity entity)
    {
        _animator = entity.GetAnimator();
        _animationEventDispatcher = entity.GetAnimationEventDispatcher();

        _attackRequsted = entity.GetAttackRequest();
        _attackAction = entity.GetAttackAction();

        _attackRequsted.Subscribe(OnAttackRequsted);
        _animationEventDispatcher.OnEventReceived += OnEventReceived;
    }


    private void OnEventReceived(string eventName)
    {
        if (eventName == "Attacked")
        {
            _attackAction.Invoke();
        }
    }

    private void OnAttackRequsted()
    {
        _animator.SetTrigger(Attack);
    }

    public void Dispose(IEntity entity)
    {
        _attackRequsted.Unsubscribe(OnAttackRequsted);
        _animationEventDispatcher.OnEventReceived -= OnEventReceived;
    }
}
