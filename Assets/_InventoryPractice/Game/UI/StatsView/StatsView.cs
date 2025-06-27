using UnityEngine;

namespace _InventoryPractice
{
    public sealed class StatsView : MonoBehaviour
    {
        [SerializeField] private StatView _healthView;
        [SerializeField] private StatView _armorView;
        [SerializeField] private StatView _aguilityView;
        [SerializeField] private StatView _powerView;


        public void SetHealth(string health)
        {
            _healthView.SetText(health);
        }

        public void SetArmor(string armor)
        {
            _armorView.SetText(armor);
        }

        public void SetAgility(string agility)
        {
            _aguilityView.SetText(agility);
        }

        public void SetPower(string power)
        {
            _powerView.SetText(power);
        }
    }
}
