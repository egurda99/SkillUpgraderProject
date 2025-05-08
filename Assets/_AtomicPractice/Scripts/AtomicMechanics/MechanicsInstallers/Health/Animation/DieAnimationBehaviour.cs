using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class DieAnimationBehaviour : IEntityInit, IEntityDispose
{
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private Animator _animator;
    private ReactiveVariable<bool> _isDead;

    public void Init(IEntity entity)
    {
        _animator = entity.GetAnimator();
        _isDead = entity.GetIsDead();

        _isDead.Subscribe(OnIsDeadChanged);
    }

    private void OnIsDeadChanged(bool value)
    {
            _animator.SetBool(IsDead,value);
    }

    public void Dispose(IEntity entity)
    {
        _isDead.Unsubscribe(OnIsDeadChanged);
    }
}
