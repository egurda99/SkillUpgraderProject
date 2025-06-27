using System;
using UnityEngine;

namespace InventoryPractice
{
    public sealed class PlayerStats : MonoBehaviour
    {
        [SerializeField] private int _power;
        [SerializeField] private int _health;
        [SerializeField] private int _aguility;
        [SerializeField] private int _armor;

        public int Power => _power;

        public int Health => _health;

        public int Aguility => _aguility;

        public int Armor => _armor;


        public event Action<int> OnPowerChanged;
        public event Action<int> OnHealthChanged;
        public event Action<int> OnAguilityChanged;
        public event Action<int> OnArmorChanged;


        public void AddPower(int power)
        {
            _power += power;
            OnPowerChanged?.Invoke(_power);
            Debug.Log($"<color=blue>Power stat changed {_power}</color>");
        }

        public void DecreasePower(int power)
        {
            _power -= power;
            OnPowerChanged?.Invoke(_power);
            Debug.Log($"<color=blue>Power stat changed {_power}</color>");
        }

        public void DecreaseHealth(int health)
        {
            _health -= health;
            OnHealthChanged?.Invoke(_health);
            Debug.Log($"<color=blue>Health stat changed {_health}</color>");
        }

        public void AddHealth(int health)
        {
            _health += health;
            OnHealthChanged?.Invoke(_health);
            Debug.Log($"<color=blue>Health stat changed {_health}</color>");
        }

        public void DecreaseAguilitty(int value)
        {
            _aguility -= value;
            OnAguilityChanged?.Invoke(_aguility);
            Debug.Log($"<color=blue>Aguility stat changed {_aguility}</color>");
        }

        public void AddAguilitty(int value)
        {
            _aguility += value;
            OnAguilityChanged?.Invoke(_aguility);
            Debug.Log($"<color=blue>Aguility stat changed {_aguility}</color>");
        }

        public void AddArmor(int value)
        {
            _armor += value;
            OnArmorChanged?.Invoke(_armor);
            Debug.Log($"<color=blue>Armor stat changed {_armor}</color>");
        }

        public void DecreaseArmor(int value)
        {
            _armor -= value;
            OnArmorChanged?.Invoke(_armor);
            Debug.Log($"<color=blue>Armor stat changed {_armor}</color>");
        }
    }
}
