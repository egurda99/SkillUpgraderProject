using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.EcsLite.Entities
{
    public sealed class EntityManager
    {
        private EcsWorld world;

        private readonly Dictionary<int, Entity> entities = new();

        public void Initialize(EcsWorld world)
        {
            var entities = GameObject.FindObjectsOfType<Entity>();
            for (int i = 0, count = entities.Length; i < count; i++)
            {
                var entity = entities[i];
                entity.Initialize(world);
                this.entities.Add(entity.Id, entity);
            }

            this.world = world;
        }

        public Entity Create(Entity prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var entity = GameObject.Instantiate(prefab, position, rotation, parent);
            entity.Initialize(world);
            entities.Add(entity.Id, entity);
            return entity;
        }

        public void Destroy(int id)
        {
            if (entities.Remove(id, out var entity))
            {
                entity.Dispose();
                GameObject.Destroy(entity.gameObject);
            }
        }

        public Entity Get(int id)
        {
            return entities[id];
        }
    }
}
