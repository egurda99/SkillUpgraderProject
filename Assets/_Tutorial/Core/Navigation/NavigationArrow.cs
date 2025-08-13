using DG.Tweening;
using UnityEngine;

namespace Game.Tutorial.Gameplay
{
    public sealed class NavigationArrow : MonoBehaviour
    {
        [SerializeField] private GameObject _rootGameObject;

        [SerializeField] private Transform _rootTransform;

        [SerializeField] private float _forwardOffset = 1.5f; // базовое смещение вперёд

        [Space] [Header("Animation parameters")] [SerializeField]
        private float _moveAmplitude = 0.3f; // амплитуда движения вперёд-назад

        [SerializeField] private float _moveDuration = 0.8f; // время одного движения вперёд-назад

        private Tween _moveTween;
        private Tween _scaleTween;
        private Vector3 _lookTarget;
        private Vector3 _baseOffsetPos;
        private Vector3 _startScale;

        private void OnEnable()
        {
            _startScale = transform.localScale;
        }

        public void Show()
        {
            _rootGameObject.SetActive(true);
            StartAnimation();
        }

        public void Hide()
        {
            StopAnimation();
            gameObject.SetActive(false);
        }

        public void SetPosition(Vector3 playerPosition)
        {
            var forwardDir = (_lookTarget - playerPosition).normalized;
            forwardDir.y = 0;

            _baseOffsetPos = playerPosition + forwardDir * _forwardOffset;
            _rootTransform.position = _baseOffsetPos;
            transform.position = _baseOffsetPos;
        }

        public void LookAt(Vector3 targetPosition, float rotationSpeed = 10f)
        {
            _lookTarget = targetPosition;

            var direction = targetPosition - _rootTransform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.001f)
            {
                var targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
                _rootTransform.rotation = Quaternion.Slerp(
                    _rootTransform.rotation,
                    targetRotation,
                    Time.deltaTime * rotationSpeed
                );
            }
        }

        private void StartAnimation()
        {
            StopAnimation();

            // Запоминаем стартовый масштаб на момент активации
            if (_startScale == Vector3.zero)
                _startScale = _rootTransform != null ? _rootTransform.localScale : transform.localScale;

            // Движение вперёд-назад по направлению стрелки
            _moveTween = DOTween.To(
                    () => 0f,
                    t =>
                    {
                        var forward = _rootTransform.forward;
                        forward.y = 0;
                        _rootTransform.position = _baseOffsetPos + forward.normalized * Mathf.Sin(t) * _moveAmplitude;
                    },
                    Mathf.PI * 2, // полный цикл синуса
                    _moveDuration
                )
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);

            // Пульсация масштаба относительно стартового размера
            _scaleTween = DOTween.To(
                    () => 1f,
                    scaleFactor => { _rootTransform.localScale = _startScale * scaleFactor; },
                    1.1f, // увеличение на 10%
                    _moveDuration / 2f
                )
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

            // На всякий случай ставим стартовый масштаб сразу
            _rootTransform.localScale = _startScale;
        }

        private void StopAnimation()
        {
            _moveTween?.Kill();
            _scaleTween?.Kill();
            _rootTransform.localScale = _startScale;
        }
    }
}
