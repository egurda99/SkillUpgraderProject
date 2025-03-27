using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(GameCycleManager))]
    public sealed class GameCycleListenersInstaller : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _listenersMonoBehaviours = new();


        public void Init()
        {
            var gameCycleManager = GetComponent<GameCycleManager>();

            AddListenersFromChildren(gameCycleManager);
            AddListenersFromMonobehaviours(gameCycleManager);
        }

        private void AddListenersFromMonobehaviours(GameCycleManager gameCycleManager)
        {
            foreach (var listenerMonoBehaviour in _listenersMonoBehaviours)
            {
                var gameListeners = listenerMonoBehaviour.GetComponents<IGameListener>();


                foreach (var listener in gameListeners)
                {
                    if (listener is IGameListener gameListener)
                    {
                        gameCycleManager.AddListener(gameListener);
                    }
                }
            }
        }

        private void AddListenersFromChildren(GameCycleManager gameCycleManager)
        {
            var listeners = GetComponentsInChildren<IGameListener>();

            foreach (var gameListener in listeners)
            {
                gameCycleManager.AddListener(gameListener);
            }
        }
    }
}
