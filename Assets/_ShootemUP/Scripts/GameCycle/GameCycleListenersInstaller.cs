using Zenject;

namespace ShootEmUp
{
    public sealed class GameCycleListenersInstaller : MonoInstaller<GameCycleListenersInstaller>
    {
        public override void InstallBindings()
        {
            foreach (var gameListener in GetComponentsInChildren<IGameListener>())
            {
                Container.BindInterfacesTo(gameListener.GetType()).FromInstance(gameListener).AsCached();
            }
        }
    }
}
