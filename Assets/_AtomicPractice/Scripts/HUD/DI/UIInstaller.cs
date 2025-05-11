using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller<UIInstaller>
{
    [SerializeField] private ValueView _hpView;
    [SerializeField] private ValueView _bulletsView;
    [SerializeField] private ValueView _killsView;
    [SerializeField] private GameOverView _gameOverView;


    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<BulletsAdapter>()
            .AsSingle()
            .WithArguments(_bulletsView);

        Container.BindInterfacesAndSelfTo<KillsAdapter>()
            .AsSingle()
            .WithArguments(_killsView);

        Container.BindInterfacesAndSelfTo<HPAdapter>()
            .AsSingle()
            .WithArguments(_hpView);

        Container.BindInterfacesAndSelfTo<GameOverAdapter>()
            .AsSingle()
            .WithArguments(_gameOverView);
    }
}
