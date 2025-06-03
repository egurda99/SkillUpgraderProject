using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Views
{
    [RequireComponent(typeof(AnimationEventDispatcher))]
    public sealed class MeleeAttackAnimationEventReciever : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        private AnimationEventDispatcher _animationDispatcher;

        private void Awake()
        {
            _animationDispatcher = GetComponent<AnimationEventDispatcher>();
        }

        private void OnEnable()
        {
            _animationDispatcher.OnEventReceived += OnEventRecieved;
        }

        private void OnDisable()
        {
            _animationDispatcher.OnEventReceived -= OnEventRecieved;
        }


        private void OnEventRecieved(string obj)
        {
            if (obj == "MeleeAttacked")
            {
                _entity.AddData(new MeleeAttackAction());
            }
        }
    }
}
