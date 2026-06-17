using Lessons.Architecture.PM;
using UnityEngine;

namespace ServiceLocator
{
    public sealed class ServiceLocatorInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var playerLevel = new PlayerLevel();
            ServiceLocator.Instance.Register<PlayerLevel>(playerLevel);
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.UnregisterAll();
        }
    }
}
