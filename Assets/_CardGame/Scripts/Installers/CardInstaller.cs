using _CardGame.Systems;
using _CardGame.View;
using UnityEngine;

namespace _CardGame.Installers
{
    public sealed class CardInstaller : MonoBehaviour
    {
        [SerializeField] private CardView _cardView;

        private HealthSystem _healthSystem;
        private AttackSystem _attackSystem;


        private void Awake()
        {
            _healthSystem = new HealthSystem(_cardView.HealthData);
            _attackSystem = new AttackSystem(_cardView.AttackData);
        }
    }
}
