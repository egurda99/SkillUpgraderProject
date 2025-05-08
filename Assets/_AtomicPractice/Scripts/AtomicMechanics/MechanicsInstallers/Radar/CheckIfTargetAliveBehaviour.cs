using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class CheckIfTargetAliveBehaviour : IEntityInit, IEntityDispose
{
    private ReactiveVariable<Transform> _target;
    private ReactiveVariable<bool> _isTargetAlive;
    private ReactiveVariable<bool> _isDead;
    private IEvent<Transform> _onTargetChanged;

    public void Init(IEntity entity)
    {
        _target = entity.GetTarget();
        _isTargetAlive = entity.GetIsTargetAlive();

        _onTargetChanged = entity.GetChangeTargetAction();

        var targetEntityProxy =  _target.Value.GetComponent<SceneEntityProxy>();

        if (targetEntityProxy != null)
        {
            _isDead = targetEntityProxy.GetIsDead();
        }

        else
        {
            var targetEntity = _target.Value.GetComponent<SceneEntity>();
            _isDead = targetEntity.GetIsDead();
        }



        _isTargetAlive.Value = !_isDead.Value;


        _onTargetChanged.Subscribe(OnTargetChanged);
        _isDead.Subscribe(OnDeadValueCheck);

    }

    private void OnTargetChanged(Transform obj)
    {
        var targetEntity =  obj.GetComponent<SceneEntityProxy>();

        if (targetEntity == null)
        {
            var targetSceneEntity =  obj.GetComponent<SceneEntity>();
            _isDead = targetSceneEntity.GetIsDead();
            _isDead.Subscribe(OnDeadValueCheck);
            OnDeadValueCheck(_isDead.Value);

        }
        else
        {
            _isDead = targetEntity.GetIsDead();
            _isDead.Subscribe(OnDeadValueCheck);
            OnDeadValueCheck(_isDead.Value);
        }
    }

    private void OnDeadValueCheck(bool value)
    {
        if (value == false)
        {
            _isTargetAlive.Value = true;
        }

        else
        {
            _isTargetAlive.Value = false;
        }
    }
    public void Dispose(IEntity entity)
    {
        _onTargetChanged.Unsubscribe(OnTargetChanged);
        _isDead.Unsubscribe(OnDeadValueCheck);
    }
}
