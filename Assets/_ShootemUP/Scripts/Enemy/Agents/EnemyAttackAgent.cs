using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(ShootComponent))]
    public sealed class EnemyAttackAgent : MonoBehaviour,
        IGameFixedUpdateListener
    {
        [SerializeField] private float _shootCountdown;

        private ShootComponent _shootComponent;
        private Transform _target;
        private Timer _timer;

        private HealthComponent _targetHealth;
        private bool _isPositionReached;

        private void Awake()
        {
            _shootComponent = GetComponent<ShootComponent>();
            _timer = new Timer();
        }

        private void OnEnable()
        {
            _isPositionReached = false;
            _timer.StartTimer(_shootCountdown);

            _timer.OnTimerEnd += ShootTimerEnd;
        }

        private void OnDisable()
        {
            _timer.OnTimerEnd -= ShootTimerEnd;
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            if (!_isPositionReached)
            {
                return;
            }

            if (!_targetHealth.IsAlive())
            {
            }
        }

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
            _timer.StartTimer(_shootCountdown);
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
