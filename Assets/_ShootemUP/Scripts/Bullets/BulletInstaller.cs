using ShootEmUp;
using UnityEngine;
using Zenject;

public class BulletInstaller : MonoInstaller
{
    [SerializeField] private Transform _bulletContainerTransform;
    [SerializeField] private int _bulletPoolInitialSize;
    [SerializeField] private Bullet _bulletPrefab;


    public override void InstallBindings()
    {
        Container.BindMemoryPool<Bullet, Bullet.Pool>().WithInitialSize(_bulletPoolInitialSize)
            .FromComponentInNewPrefab(_bulletPrefab).UnderTransform(_bulletContainerTransform).AsCached();

        //   Container.Bind<ActiveBulletsProvider>().AsSingle();
    }
}
