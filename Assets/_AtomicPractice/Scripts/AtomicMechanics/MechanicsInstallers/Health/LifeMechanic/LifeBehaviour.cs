using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public class LifeBehaviour : IEntityInit, IEntityDispose
{
    private ReactiveVariable<float> _hitPoints;
    private ReactiveVariable<bool> _isDead;

    private IEvent<float> _takeDamageEvent;
    private IEvent<float> _healEvent;


    public void Init(IEntity entity)
    {
        _hitPoints = entity.GetHitPoints();
        _isDead = entity.GetIsDead();

        _takeDamageEvent = entity.GetTakeDamageAction();

        _healEvent = entity.GetHealAction();


        _healEvent.Subscribe(OnHeal);
        _takeDamageEvent.Subscribe(OnTakeDamage);


    }

    private void OnHeal(float heal)
    {
        if (_isDead.Value)
        {
            return;
        }

        _hitPoints.Value += heal;

    }

    private void OnTakeDamage(float damage)
    {
        Debug.Log($"<color=orange>Damaged on {damage}!</color>");
        if (_isDead.Value)
        {
            return;
        }

        _hitPoints.Value -= damage;

        if (_hitPoints.Value <= 0)
        {
            _hitPoints.Value = 0;
            _isDead.Value = true;
            Debug.Log($"<color=red>Died!</color>");
        }
    }

    public void Dispose(IEntity entity)
    {
        _healEvent.Unsubscribe(OnHeal);
        _takeDamageEvent.Unsubscribe(OnTakeDamage);
    }
}
