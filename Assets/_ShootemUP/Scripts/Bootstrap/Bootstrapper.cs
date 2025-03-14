using ShootEmUp;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [Header("Input")] [SerializeField] private KeyboardInput _keyboardInput;

    [Header("Bullet system")] [SerializeField]
    private BulletPool _bulletPool;

    [SerializeField] private BulletOutOfBoundsChecker _bulletOutOfBoundsChecker;

    [Header("Player")] [SerializeField] private Transform _playerTransform;

    [Header("Enemy system")] [SerializeField]
    private EnemyPositionsHandler enemyPositionsHandler;

    [SerializeField] private EnemySpawner _enemySpawner;


    private CharacterDeathObserver _playerDeathObserver;
    private GameFinisher _gameFinisher;
    private ActiveBulletsProvider _activeBulletsProvider;
    private BulletOutOfBoundsObserver _bulletOutOfBoundsObserver;
    private HealthComponent _playerHealthComponent;
    private MoveComponent _playerMoveComponent;
    private MoveController _playerMoveController;
    private ShootController _playerShootController;
    private ShootComponent _playerShootComponent;
    private EnemyInstaller _enemyInstaller;

    private void Awake()
    {
        _gameFinisher = new GameFinisher();

        PlayerInit();
        BulletInit();
        EnemyInit();
    }

    private void EnemyInit()
    {
        _enemyInstaller = new EnemyInstaller(_playerTransform, enemyPositionsHandler);
        _enemySpawner.Init(_enemyInstaller);
    }

    private void BulletInit()
    {
        _bulletPool.Init();

        _activeBulletsProvider = new ActiveBulletsProvider(_bulletPool);
        _bulletOutOfBoundsChecker.Init(_activeBulletsProvider);
        _bulletOutOfBoundsObserver = new BulletOutOfBoundsObserver(_bulletPool, _bulletOutOfBoundsChecker);
    }

    private void PlayerInit()
    {
        _playerHealthComponent = _playerTransform.GetComponent<HealthComponent>();
        _playerMoveComponent = _playerTransform.GetComponent<MoveComponent>();
        _playerShootComponent = _playerTransform.GetComponent<ShootComponent>();
        _playerMoveController = new MoveController(_playerMoveComponent, _keyboardInput);
        _playerShootController = new ShootController(_playerShootComponent, _keyboardInput);
        _playerDeathObserver = new CharacterDeathObserver(_gameFinisher, _playerHealthComponent);
    }
}
