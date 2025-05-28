using System;
using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Installer
{
    public sealed class CharacterInstaller : EntityInstaller
    {
        [SerializeField] private Text _text;
        [SerializeField] private float _moveSpeed = 5.0f;
        [SerializeField] private int _health = 5;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Entity _bulletPrefab;
        private Entity _entity;
        private int _currentHealth;

        protected override void Install(Entity entity)
        {
            _entity = entity;
            entity.AddData(new Position {Value = transform.position});
            entity.AddData(new Rotation {Value = transform.rotation});
            entity.AddData(new MoveDirection {Value = Vector3.forward});
            entity.AddData(new MoveSpeed {Value = _moveSpeed});
            entity.AddData(new TransformView { Value = transform});
            entity.AddData(new AnimatorView { Value = _animator});
            entity.AddData(new Health { Value = _health});
            entity.AddData(new DamageableTag());
            entity.AddData(new BulletWeapon
            {
                FirePoint = _firePoint,
                BulletPrefab = _bulletPrefab
            });
        }

        private void Update()
        {
            if (_entity == null)
            {
                return;
            }
            
            if (_entity.TryGetData(out Health health))
            {
                if (!_currentHealth.Equals(health.Value))
                {
                    _currentHealth = health.Value;
                    _text.text = _currentHealth.ToString();
                }
            }
        }

        protected override void Dispose(Entity entity)
        {
        }
    }
}
