using System;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class TwoD_MoveAnimationMechanic : IEntityInstaller
{
    [SerializeField] private Animator _animator;

    public void Install(IEntity entity)
    {
        entity.AddAnimator(_animator);
        entity.AddBehaviour(new TwoD_MoveAnimationBehaviour());
    }
}
