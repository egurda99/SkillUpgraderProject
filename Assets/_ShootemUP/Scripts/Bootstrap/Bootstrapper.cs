using ShootEmUp;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [Header("GameCycle")] [SerializeField] private GameCycleInstaller _gameCycleInstaller;
    [SerializeField] private GameCycleManager _gameCycleManager;

    [Header("Input")] [SerializeField] private KeyboardInput _keyboardInput;

    [Header("Bullet system")] [SerializeField]
    private BulletPool _bulletPool;

    [SerializeField] private BulletOutOfBoundsChecker _bulletOutOfBoundsChecker;

    [Header("Player")] [SerializeField] private Transform _playerTransform;

    [Header("Enemy system")] [SerializeField]
    private EnemyPositionsHandler _enemyPositionsHandler;

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private EnemyPool _enemyPool;

    [Header("UI")] [SerializeField] private StartGameWidget _startGameWidget;

    [SerializeField] private PauseGameWidget _pauseGameWidget;


    private CharacterDeathObserver _playerDeathObserver;
    private GameFinisher _gameFinisher;
    private ActiveBulletsProvider _activeBulletsProvider;
    private BulletsGameCycleUpdater _bulletsGameCycleUpdater;
    private BulletOutOfBoundsObserver _bulletOutOfBoundsObserver;
    private HealthComponent _playerHealthComponent;
    private MoveComponent _playerMoveComponent;
    private MoveController _playerMoveController;
    private ShootController _playerShootController;
    private ShootComponent _playerShootComponent;
    private EnemyInstaller _enemyInstaller;
    private ActiveEnemiesProvider _activeEnemiesProvider;
    private EnemiesGameCycleUpdater _enemiesGameCycleUpdater;

    private GameCycleWidgetsController _gameCycleWidgetsController;

    private void Awake()
    {
        _gameFinisher = new GameFinisher(_gameCycleManager);

        PlayerInit();
        BulletInit();
        EnemyInit();

        _gameCycleWidgetsController =
            new GameCycleWidgetsController(_startGameWidget, _pauseGameWidget, _gameCycleManager);

        _gameCycleInstaller.Init();
    }

    private void EnemyInit()
    {
        _enemyInstaller = new EnemyInstaller(_playerTransform, _enemyPositionsHandler);
        _enemySpawner.Init(_enemyInstaller);
        _activeEnemiesProvider = new ActiveEnemiesProvider(_enemyPool);
        _enemiesGameCycleUpdater = new EnemiesGameCycleUpdater(_activeEnemiesProvider);

        _gameCycleManager.AddListener(_enemiesGameCycleUpdater);
    }

    private void BulletInit()
    {
        _bulletPool.Init();

        _activeBulletsProvider = new ActiveBulletsProvider(_bulletPool);
        _bulletsGameCycleUpdater = new BulletsGameCycleUpdater(_activeBulletsProvider);
        _bulletOutOfBoundsChecker.Init(_activeBulletsProvider);
        _bulletOutOfBoundsObserver = new BulletOutOfBoundsObserver(_bulletPool, _bulletOutOfBoundsChecker);

        _gameCycleManager.AddListener(_bulletsGameCycleUpdater);
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
