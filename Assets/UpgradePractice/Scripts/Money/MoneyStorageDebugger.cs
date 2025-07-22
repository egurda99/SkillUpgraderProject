using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _UpgradePractice.Scripts
{
    public sealed class MoneyStorageDebugger : MonoBehaviour
    {
        private MoneyStorage _moneyStorage;

        [Inject]
        public void Construct(MoneyStorage storage)
        {
            _moneyStorage = storage;
            _moneyStorage.SetupMoney(100000);
        }

        [Button]
        public void SetupMoney(int money)
        {
            _moneyStorage.SetupMoney(money);
        }
    }
}
