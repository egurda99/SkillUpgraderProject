using Client.Components;
using Leopotam.EcsLite.Entities;
using Scripts.Proxy;
using UnityEngine;

namespace Client.Views
{
    [RequireComponent(typeof(Entity))]
    public sealed class BulletCollisionComponent : MonoBehaviour
    {
        private Entity _entity;

        private void Awake()
        {
            _entity = GetComponent<Entity>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out EntityProxy proxy))
            {
                var target = proxy.Entity;

                var currentTeam = _entity.GetData<Team>();
                var targetTeam = target.GetData<Team>();

                Debug.Log($"ON COLLISION ENTER {target.Id}", this);

                if (currentTeam.Value == targetTeam.Value)
                    return;


                #region 1

                // EcsWorld ecsWorld = EcsStartup.Instance.GetWorld(EcsWorlds.EVENTS);
                // int newEntity = ecsWorld.NewEntity();
                //
                // EcsPool<CollisionEnterRequest> ecsPool = ecsWorld.GetPool<CollisionEnterRequest>();
                // ecsPool.Add(newEntity) = new CollisionEnterRequest();

                #endregion

                #region 2

                EcsStartup.Instance.CreateEntity(EcsWorlds.EVENTS)
                    .Add(new CollisionEnterRequest())
                    .Add(new BulletTag())
                    .Add(new SourceEntity { Value = _entity.Id })
                    .Add(new TargetEntity { Value = target.Id })
                    .Add(new Position { Value = collision.GetContact(0).point });

                #endregion
            }
        }
    }
}
