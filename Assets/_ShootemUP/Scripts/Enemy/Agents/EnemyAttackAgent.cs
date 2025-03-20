using ShootemUP;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour,
        IGameStartListener,
        IGameFinishListener,
        IGameUpdateListener
    {
        [SerializeField] private float _countdown;

        private ShootComponent _shootComponent;
        private Transform _target;
        private Timer _timer;

        private HealthComponent _targetHealth;
        private bool _isPositionReached;

        private void Awake()
        {
            _shootComponent = GetComponent<ShootComponent>();
            _timer = new Timer();
            _timer.StartTimer(_countdown);
        }

        private void OnEnable()
        {
            _isPositionReached = false;
            _timer.OnTimerEnd += ShootTimerEnd;
        }

        private void OnDisable()
        {
            _timer.OnTimerEnd -= ShootTimerEnd;
        }

        void IGameStartListener.OnStartGame()
        {
            // _isPositionReached = false;
            // _timer.OnTimerEnd += ShootTimerEnd;
        }

        void IGameFinishListener.OnFinishGame()
        {
            //_timer.OnTimerEnd -= ShootTimerEnd;
        }

        void IGameUpdateListener.OnUpdate(float deltaTime)
        {
            Debug.Log("_isPositionReached" + _isPositionReached);
            if (!_isPositionReached)
            {
                return;
            }

            if (!_targetHealth.IsAlive())
            {
                return;
            }

            _timer.UpdateTimer(deltaTime);
        }

        // void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        // {
        //     Debug.Log("_isPositionReached" + _isPositionReached);
        //     if (!_isPositionReached)
        //     {
        //         return;
        //     }
        //
        //     if (!_targetHealth.IsAlive())
        //     {
        //         return;
        //     }
        //
        //     _timer.UpdateTimer(Time.deltaTime);
        // }

        public void SetTarget(Transform target)
        {
            _target = target;
            _targetHealth = target.GetComponent<HealthComponent>();
        }

        public void SetPositionReached()
        {
            _isPositionReached = true;
        }


        private void ShootTimerEnd()
        {
            Fire();
            _timer.StartTimer(_countdown);
        }

        private void Fire()
        {
            var startPosition = _shootComponent.GetShootPosition();
            var vector = (Vector2)_target.transform.position - startPosition;
            var direction = vector.normalized;
            _shootComponent.Shoot(direction);
        }
    }
}
