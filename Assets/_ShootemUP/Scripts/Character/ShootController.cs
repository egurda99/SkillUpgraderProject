using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class ShootController : IDisposable
    {
        private readonly ShootComponent _shootComponent;

        private readonly Vector2 _direction = Vector2.up;

        private IInput _input;

        public ShootController(ShootComponent shootComponent)
        {
            _shootComponent = shootComponent;
        }

        [Inject]
        public void Construct(IInput input)
        {
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
