using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IInput
    {
        event Action<Vector2> OnMoveInputChanged;
        event Action OnFireClicked;
    }
}