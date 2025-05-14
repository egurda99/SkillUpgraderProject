using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class StunVFXBehaviour : IEntityInit, IEntityDispose
{
    private ReactiveVariable<bool> _isStunned;
    private ParticleSystem _stunFx;


    public void Init(IEntity entity)
    {
        _isStunned = entity.GetIsStunned();
        _stunFx = entity.GetStunFX();

        _isStunned.Subscribe(OnIsStunnedChanged);
    }

    private void OnIsStunnedChanged(bool value)
    {
        if (value)
        {
            _stunFx.Play();
        }

        else
        {
            _stunFx.Stop();
            _stunFx.Clear();
        }
    }


    public void Dispose(IEntity entity)
    {
        _isStunned.Unsubscribe(OnIsStunnedChanged);
    }
}
