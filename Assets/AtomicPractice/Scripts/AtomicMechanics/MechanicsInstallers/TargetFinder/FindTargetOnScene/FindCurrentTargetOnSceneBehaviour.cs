using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class FindCurrentTargetOnSceneBehaviour : IEntityInit
{
    private ReactiveVariable<bool> _isFound;
    private ReactiveVariable<Transform> _target;

    public void Init(IEntity entity)
    {
        _isFound = entity.GetIsFound();
        _target = entity.GetTarget();

        if (_target.Value == null)
        {
            _isFound.Value = false;
            return;
        }

        var allTransforms = Object.FindObjectsOfType<Transform>();

        foreach (var t in allTransforms)
        {
            if (t == _target.Value)
            {
                _isFound.Value = true;
                return;
            }
        }

        _isFound.Value = false;
    }
}
