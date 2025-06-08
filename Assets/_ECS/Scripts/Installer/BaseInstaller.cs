using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Installer
{
    public sealed class BaseInstaller : EntityInstaller
    {
        [SerializeField] private TeamMember _teamMember;

        [SerializeField] private int _health = 5;

        [Space] [Header("UI")] [SerializeField]
        private Image _hpImage;


        private Entity _entity;
        private int _currentHealth;

        protected override void Install(Entity entity)
        {
            _entity = entity;
            entity.AddData(new Health { Value = _health });
            entity.AddData(new Position { Value = transform.position });

            entity.AddData(new DamageableTag());
            entity.AddData(new BaseTag());

            entity.AddData(new Team { Value = _teamMember });
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
