using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class DestroyByLifeTimeBehaviour : IEntityInit, IEntityUpdate
{
    private ReactiveVariable<float> _lifeTime;
    private Transform _rootTransform;
    private float _currentTime;

    public void Init(IEntity entity)
    {
        _lifeTime = entity.GetLifeTime();
        _rootTransform = entity.GetRootTransform();

        _currentTime = _lifeTime.Value;
    }


    public void OnUpdate(IEntity entity, float deltaTime)
    {
        _currentTime -= deltaTime;
        if (_currentTime <= 0)
        {
            _currentTime = 0;
            Object.Destroy(_rootTransform.gameObject);
        }
    }
}