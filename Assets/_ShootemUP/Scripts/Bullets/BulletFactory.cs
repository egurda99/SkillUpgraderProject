// using UnityEngine;
// using Zenject;
//
// namespace ShootEmUp
// {
//     public sealed class BulletFactory : MonoBehaviour
//     {
//         [SerializeField] private Bullet _bulletPrefab;
//
//         private BulletCunfigurer _bulletCunfigurer;
//
//         [Inject]
//         public void Construct(BulletCunfigurer bulletCunfigurer)
//         {
//             _bulletCunfigurer = bulletCunfigurer;
//         }
//
//         public Bullet GetBullet(Transform parent)
//         {
//             var bullet = Instantiate(_bulletPrefab, parent);
//             var bulletDamageController = new BulletDamageController(bullet, _bulletCunfigurer);
//             Debug.Log($"<color=red>Controller created with args: {bullet}, + {_bulletCunfigurer} < /color>");
//             return bullet;
//         }
//     }
// }
