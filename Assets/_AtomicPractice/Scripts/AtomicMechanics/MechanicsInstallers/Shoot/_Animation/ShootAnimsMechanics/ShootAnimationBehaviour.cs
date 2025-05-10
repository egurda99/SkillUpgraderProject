using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class ShootAnimationBehaviour : IEntityInit, IEntityDispose
{
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private Animator _animator;
    private AnimationEventDispatcher _animationEventDispatcher;
    private IEvent _shootRequsted;
    private IEvent _shootAction;
    private AndExpression _canShoot;

    public void Init(IEntity entity)
    {
        _animator = entity.GetAnimator();
        _animationEventDispatcher = entity.GetAnimationEventDispatcher();
        _shootRequsted = entity.GetShootRequest();
        _shootAction = entity.GetShootAction();
        _canShoot = entity.GetCanShoot();


        _shootRequsted.Subscribe(OnShootRequsted);
        _animationEventDispatcher.OnEventReceived += OnEventReceived;
    }

    private void OnEventReceived(string eventName)
    {
        if (eventName == "Shoot")
        {
            _shootAction.Invoke();
        }
    }

    private void OnShootRequsted()
    {
        if (_canShoot.Value)
        {
            _animator.SetTrigger(Shoot);
        }
    }

    public void Dispose(IEntity entity)
    {
        _shootRequsted.Unsubscribe(OnShootRequsted);
        _animationEventDispatcher.OnEventReceived -= OnEventReceived;
    }
}
