using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class StunAnimationBehaviour : IEntityInit, IEntityDispose
{
    private static readonly int IsStunned = Animator.StringToHash("IsStunned");
    private Animator _animator;
    private ReactiveVariable<bool> _isStunned;


    public void Init(IEntity entity)
    {
        _animator = entity.GetAnimator();
        _isStunned = entity.GetIsStunned();

        _isStunned.Subscribe(OnIsStunnedChanged);
    }

    private void OnIsStunnedChanged(bool value)
    {
        _animator.SetBool(IsStunned, value);
    }


    public void Dispose(IEntity entity)
    {
        _isStunned.Unsubscribe(OnIsStunnedChanged);
    }
}
