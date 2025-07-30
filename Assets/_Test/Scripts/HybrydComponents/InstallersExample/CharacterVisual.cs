using UnityEngine;

namespace MyCodeBase.HybridComponents
{
    public sealed class CharacterVisual : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;

        private MoveAnimationSystem _moveAnimationSystem;
        private DeathAnimationSystem _deathAnimationSystem;

        private void Start()
        {
            _moveAnimationSystem = new MoveAnimationSystem(_character.MoveData, _animator);
            _deathAnimationSystem = new DeathAnimationSystem(_character.HealthData, _animator);
        }

        private void OnDestroy()
        {
            _moveAnimationSystem.Dispose();
            _deathAnimationSystem.Dispose();
        }
    }
}
