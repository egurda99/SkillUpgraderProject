using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class TakeDamageVFXBehaviour : IEntityInit, IEntityDispose
{
    private IEvent<float> _takeDamageAction;
    private ParticleSystem _takeDamageFx;
    private Transform _rootTransform;


    public void Init(IEntity entity)
    {
        _takeDamageAction = entity.GetTakeDamageAction();
        _takeDamageFx = entity.GetTakeDamageFX();
        _rootTransform = entity.GetRootTransform();

        _takeDamageAction.Subscribe(OnTakeDamageEvent);
    }

    private void OnTakeDamageEvent(float damage)
    {
        _takeDamageFx.transform.forward = -_rootTransform.forward;

        _takeDamageFx.Play();
    }

    public void Dispose(IEntity entity)
    {
        _takeDamageAction.Unsubscribe(OnTakeDamageEvent);
    }
}
