using ShootEmUp;
using UnityEngine;
using Zenject;

public sealed class Bootstrapper : MonoBehaviour
{
    private GameCycleManager _gameCycleManager;

    [Header("Input")] [SerializeField] private KeyboardInput _keyboardInput;


    [Header("Player")] [SerializeField] private Transform _playerTransform;

    [Header("Enemy system")] [SerializeField]
    private EnemyPositionsHandler _enemyPositionsHandler;

    [SerializeField] private EnemySpawner _enemySpawner;

    [Header("UI")] [SerializeField] private StartGameWidget _startGameWidget;

    [SerializeField] private PauseGameWidget _pauseGameWidget;
    [SerializeField] private TimerBeforeStartWidget _timerBeforeStartWidget;


    private CharacterDeathObserver _playerDeathObserver;
    private GameFinisher _gameFinisher;

    private HealthComponent _playerHealthComponent;
    private MoveComponent _playerMoveComponent;
    private MoveController _playerMoveController;
    private ShootController _playerShootController;
    private ShootComponent _playerShootComponent;
    private EnemyConfigurer _enemyConfigurer;
    private ActiveEnemiesProvider _activeEnemiesProvider;
    private EnemiesGameCycleUpdater _enemiesGameCycleUpdater;

    private GameCycleWidgetsHandler _gameCycleWidgetsHandler;

    private Bullet.Pool _bulletPool;
    private Enemy.Pool _enemyPool;

    [Inject]
    public void Construct(Bullet.Pool bulletPool, GameCycleManager gameCycleManager, Enemy.Pool enemyPool)
    {
        _bulletPool = bulletPool;
        _gameCycleManager = gameCycleManager;
        _enemyPool = enemyPool;
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
        _enemyConfigurer = new EnemyConfigurer(_playerTransform, _enemyPositionsHandler, _bulletPool, _enemyPool);
        _activeEnemiesProvider = new ActiveEnemiesProvider(_enemyPool);
        _enemySpawner.Init(_enemyConfigurer, _activeEnemiesProvider);
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
