using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Installer
{
    internal sealed class BulletInstaller : EntityInstaller
    {
        [SerializeField] private float _moveSpeed = 3.0f;
        [SerializeField] private int _damage = 3;

        protected override void Install(Entity entity)
        {
            entity.AddData(new BulletTag());
            entity.AddData(new Position { Value = transform.position });
            entity.AddData(new Rotation { Value = transform.rotation });
            entity.AddData(new MoveSpeed { Value = _moveSpeed });
            entity.AddData(new Damage { Value = _damage });
            entity.AddData(new TransformView { Value = transform });
        }

        protected override void Dispose(Entity entity)
        {
        }
    }
}
