using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Installer
{
    public sealed class WarriorInstaller : EntityInstaller
    {
        [SerializeField] private float _moveSpeed = 5.0f;
        [SerializeField] private float _damage = 1.0f;
        [SerializeField] private float _attackRange = 2.0f;
        [SerializeField] private float _attackÑooldown = 3.0f;
        [SerializeField] private TeamMember _teamMember;

        [SerializeField] private int _health = 5;
        [SerializeField] private Animator _animator;

        [Space] [Header("UI")] [SerializeField]
        private Image _hpImage;


        private Entity _entity;
        private int _currentHealth;

        protected override void Install(Entity entity)
        {
            _entity = entity;
            entity.AddData(new Position { Value = transform.position });
            entity.AddData(new Rotation { Value = transform.rotation });
            entity.AddData(new MoveDirection { Value = Vector3.zero });
            entity.AddData(new MoveSpeed { Value = _moveSpeed });
            entity.AddData(new TransformView { Value = transform });
            entity.AddData(new AnimatorView { Value = _animator });
            entity.AddData(new Health { Value = _health });
            entity.AddData(new DamageableTag());
            entity.AddData(new MeleeWeapon { Damage = _damage });

            entity.AddData(new Team { Value = _teamMember });
            entity.AddData(new AttackRange { Value = _attackRange });
            entity.AddData(new AttackCooldown { Value = _attackÑooldown });
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

                    if (_currentHealth > 0)
                    {
                        _hpImage.fillAmount = (float)_currentHealth / _health;
                    }

                    else
                    {
                        _hpImage.fillAmount = 0;
                    }
                }
            }
        }

        protected override void Dispose(Entity entity)
        {
        }
    }
}
