using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Scripts.Proxy
{
    public class EntityProxy : MonoBehaviour
    {
        [SerializeField] private Entity _entity;

        public Entity Entity => _entity;
    }
}
