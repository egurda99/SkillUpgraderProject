using ShootEmUp;
using UnityEngine;
using Zenject;

public class BulletInstaller : MonoInstaller
{
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField] private Transform _bulletContainerTransform;

    public override void InstallBindings()
    {
        // Container.Bind<BulletPool>().FromComponentInNewPrefab(_bulletPool).AsSingle().NonLazy();
        Container.Bind<BulletPool>().FromInstance(_bulletPool).AsSingle();
        //_bulletPool.Init(_bulletFactory, _bulletContainerTransform);


        // var bulletPool = Container.InstantiatePrefabForComponent<BulletPool>(_bulletPool);
        // bulletPool.Init(_bulletFactory, _bulletContainerTransform);
        //
        //
        // Container.Bind<BulletPool>().FromInstance(bulletPool).AsSingle();

        // Container.BindInterfacesAndSelfTo<BulletDamageController>().AsTransient();
    }
}
