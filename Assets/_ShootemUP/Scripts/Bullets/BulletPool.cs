// using System;
// using UnityEngine;
// using Zenject;
//
// namespace ShootEmUp
// {
// //     public sealed class BulletCunfigurer : MonoBehaviour
// //     {
// //         private Bullet.Pool _pool;
// //         // [SerializeField] private int _initialCount = 50;
// //         //
// //         // // [SerializeField] private Transform _bulletContainerTransform;
// //         // private Transform _bulletContainerTransform;
// //         //
// //         // // [SerializeField] private BulletFactory _bulletFactory;
// //         // private BulletFactory _bulletFactory;
// //         //
// //         // private readonly Queue<GameObject> _bulletPool = new();
// //
// //         public event Action<Bullet> OnBulletSpawned;
// //         public event Action<Bullet> OnBulletDespawned;
// //
// //
// //         [Inject]
// //         public void Construct(Bullet.Pool pool)
// //         {
// //             _pool = pool;
// //         }
// //
// //         // public void Init(BulletFactory bulletFactory, Transform bulletContainerTransform)
// //         // {
// //         //     _bulletFactory = bulletFactory;
// //         //     _bulletContainerTransform = bulletContainerTransform;
// //         //
// //         //     for (var i = 0; i < _initialCount; i++)
// //         //     {
// //         //         var bullet = _bulletFactory.GetBullet(_bulletContainerTransform);
// //         //         _bulletPool.Enqueue(bullet.gameObject);
// //         //         ToggleActiveStatus(bullet.gameObject, false);
// //         //     }
// //         // }
// //
// //         public GameObject SpawnBullet(Vector2 spawnPosition, bool isPlayer, Vector2 direction)
// //         {
// //             // if (!_bulletPool.TryDequeue(out var bullet))
// //             // {
// //             //     Debug.LogWarning("BulletPool null");
// //             //     return null;
// //             // }
// //
// //             _pool.Spawn(spawnPosition);
// //
// //
// //             ToggleActiveStatus(bullet, true);
// //             var bulletComponent = bullet.GetComponent<Bullet>();
// //             bulletComponent.Init(spawnPosition, direction, isPlayer);
// //             OnBulletSpawned?.Invoke(bulletComponent);
// //             return bullet;
// //         }
// //
// //         public void DespawnBullet(Bullet bullet)
// //         {
// //             bullet.transform.SetParent(_bulletContainerTransform);
// //             ToggleActiveStatus(bullet.gameObject, false);
// //             _bulletPool.Enqueue(bullet.gameObject);
// //             OnBulletDespawned?.Invoke(bullet);
// //         }
// //
// //         private void ToggleActiveStatus(GameObject bullet, bool isActive) => bullet.SetActive(isActive);
// //     }
// // }
