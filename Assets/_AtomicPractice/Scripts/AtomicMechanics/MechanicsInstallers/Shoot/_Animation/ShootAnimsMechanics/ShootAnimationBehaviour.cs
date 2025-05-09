using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class ShootAnimationBehaviour : IEntityInit, IEntityDispose
{
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private const int AimLayerIndex = 1;

    private Animator _animator;
    private AnimationEventDispatcher _animationEventDispatcher;
    private IEvent _shootRequsted;
    private IEvent _shootAction;

    public void Init(IEntity entity)
    {
        _animator = entity.GetAnimator();
        _animationEventDispatcher = entity.GetAnimationEventDispatcher();
        _shootRequsted = entity.GetShootRequest();
        _shootAction = entity.GetShootAction();


        _shootRequsted.Subscribe(OnShootRequsted);
        _animationEventDispatcher.OnEventReceived += OnEventReceived;
    }

    private void OnEventReceived(string eventName)
    {
        if (eventName == "Shoot")
        {
            _shootAction.Invoke();
            _animator.SetLayerWeight(AimLayerIndex, 0);
        }
    }

    private void OnShootRequsted()
    {
        if (!_animator.GetBool(IsMoving))
        {
            _animator.SetTrigger(Shoot);
        }

        _animator.SetLayerWeight(AimLayerIndex, 1);
    }

    public void Dispose(IEntity entity)
    {
        _shootRequsted.Unsubscribe(OnShootRequsted);
        _animationEventDispatcher.OnEventReceived -= OnEventReceived;
    }
}
