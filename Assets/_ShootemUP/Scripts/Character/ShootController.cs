using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class ShootController : IDisposable
    {
        private readonly ShootComponent _shootComponent;

        private readonly Vector2 _direction = Vector2.up;

        private readonly IInput _input;

        public ShootController(ShootComponent shootComponent, IInput input)
        {
            _shootComponent = shootComponent;
            _input = input;
            _input.OnFireClicked += Shoot;
        }


        void IDisposable.Dispose()
        {
            _input.OnFireClicked -= Shoot;
        }

        private void Shoot() => _shootComponent.Shoot(_direction);
    }
}
