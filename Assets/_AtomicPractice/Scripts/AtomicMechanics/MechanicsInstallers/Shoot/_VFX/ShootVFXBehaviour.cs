using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class ShootVFXBehaviour : IEntityInit, IEntityDispose
{
    private IEvent _shootEvent;
    private ParticleSystem _shootFx;


    public void Init(IEntity entity)
    {
        _shootEvent = entity.GetShootEvent();
        _shootFx = entity.GetShootFX();

        _shootEvent.Subscribe(OnShootEvent);
    }

    private void OnShootEvent()
    {
        _shootFx.Play();
    }

    public void Dispose(IEntity entity)
    {
        _shootEvent.Unsubscribe(OnShootEvent);
    }
}
