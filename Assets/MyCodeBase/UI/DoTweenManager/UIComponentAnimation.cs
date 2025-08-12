using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MyCodeBase.UI
{
    public sealed class UIComponentAnimation : MonoBehaviour
    {
        [SerializeField] private Graphic _graphic;

        [SerializeField] private float _scaleMultiplier = 1.1f;
        [SerializeField] private float _duration = 0.5f;

        private DoTweenAnimationManager _animationManager;

        [Inject]
        public void Construct(DoTweenAnimationManager animationManager)
        {
            _animationManager = animationManager;
        }

        private void OnEnable()
        {
            _animationManager.StartIdleScale(_graphic, _scaleMultiplier, _duration);
        }
    }
}