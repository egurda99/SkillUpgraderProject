using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class ShootAnimationMechanic : IEntityInstaller
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationEventDispatcher _animationEventDispatcher;

    private IEvent _shootRequested;

    public void Install(IEntity entity)
    {
        entity.AddAnimator(_animator);
        entity.AddAnimationEventDispatcher(_animationEventDispatcher);

        entity.AddBehaviour(new ShootAnimationBehaviour());
    }
}
