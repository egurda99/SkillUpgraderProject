namespace _CardGame.Installers
{
    // [RequireComponent(typeof(HeroView))]
    // public sealed class CardInstaller : MonoBehaviour, IDisposable
    // {
    //     [SerializeField] private CardView _cardView;
    //
    //     private HealthSystem _healthSystem;
    //     private AttackSystem _attackSystem;
    //     private CardStatAdapter _cardStatAdapter;
    //
    //     public CardView CardView => _cardView;
    //
    //     public HealthSystem HealthSystem => _healthSystem;
    //
    //     public AttackSystem AttackSystem => _attackSystem;
    //
    //
    //     private void Awake()
    //     {
    //         var view = GetComponent<HeroView>();
    //
    //         //    _healthSystem = new HealthSystem(_cardView.HealthData);
    //         _attackSystem = new AttackSystem(_cardView.AttackData);
    //         _cardStatAdapter = new CardStatAdapter(_cardView.HealthData, _cardView.AttackData, view);
    //     }
    //
    //     public void Dispose()
    //     {
    //         _cardStatAdapter.Dispose();
    //     }
    // }
}
