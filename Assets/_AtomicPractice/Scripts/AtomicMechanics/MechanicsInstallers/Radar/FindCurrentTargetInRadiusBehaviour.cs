using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class FindCurrentTargetInRadiusBehaviour : IEntityInit, IEntityUpdate
{
    private ReactiveVariable<float> _radius;
    private Transform _root;
    private ReactiveVariable<bool> _isFound;
    private ReactiveVariable<Transform> _target;

    public void Init(IEntity entity)
    {
        _radius = entity.GetRadius();
        _root = entity.GetRootTransform();
        _isFound = entity.GetIsFound();
        _target = entity.GetTarget();
    }

    public void OnUpdate(IEntity entityRoot, float deltaTime)
    {
        Collider[] colliders = Physics.OverlapSphere(_root.position, _radius.Value);
        bool foundTarget = false;

        for (int i = 0; i < colliders.Length; i++)
        {
            Transform hitTransform = colliders[i].transform;
            if (hitTransform == _target.Value || hitTransform.IsChildOf(_target.Value))
            {
                foundTarget = true;
                break;
            }
        }

        _isFound.Value = foundTarget;
    }
}
