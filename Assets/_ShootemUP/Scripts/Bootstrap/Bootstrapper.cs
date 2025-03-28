using ShootEmUp;
using UnityEngine;
using Zenject;

public sealed class Bootstrapper : MonoBehaviour
{
    // [Header("GameCycle")] [SerializeField] private GameCycleListenersInstaller _gameCycleListenersInstaller;

    private GameCycleManager _gameCycleManager;

    [Header("Input")] [SerializeField] private KeyboardInput _keyboardInput;


    [SerializeField] private Transform _bulletContainerTransform;

    [SerializeField] private BulletOutOfBoundsChecker _bulletOutOfBoundsChecker;

    [Header("Player")] [SerializeField] private Transform _playerTransform;

    [Header("Enemy system")] [SerializeField]
    private EnemyPositionsHandler _enemyPositionsHandler;

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private EnemyPool _enemyPool;

    [Header("UI")] [SerializeField] private StartGameWidget _startGameWidget;

    [SerializeField] private PauseGameWidget _pauseGameWidget;
    [SerializeField] private TimerBeforeStartWidget _timerBeforeStartWidget;


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

    private GameCycleWidgetsHandler _gameCycleWidgetsHandler;

    private Bullet.Pool _bulletPool;

    [Inject]
    public void Construct(Bullet.Pool bulletPool, GameCycleManager gameCycleManager)
    {
        _bulletPool = bulletPool;
        _gameCycleManager = gameCycleManager;
    }

    private void Awake()
    {
        _gameFinisher = new GameFinisher(_gameCycleManager);

        PlayerInit();
        EnemyInit();

        _gameCycleWidgetsHandler =
            new GameCycleWidgetsHandler(_startGameWidget, _pauseGameWidget, _gameCycleManager,
                _timerBeforeStartWidget);
    }

    private void EnemyInit()
    {
        _enemyInstaller = new EnemyInstaller(_playerTransform, _enemyPositionsHandler, _bulletPool);
        _enemySpawner.Init(_enemyInstaller);
        _activeEnemiesProvider = new ActiveEnemiesProvider(_enemyPool);
        _enemiesGameCycleUpdater = new EnemiesGameCycleUpdater(_activeEnemiesProvider);

        _gameCycleManager.AddListener(_enemiesGameCycleUpdater);
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
