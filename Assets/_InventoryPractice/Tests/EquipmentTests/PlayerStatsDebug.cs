using System;
using UnityEngine;

namespace InventoryPractice
{
    public sealed class PlayerStatsDebug
    {
        private int _power;
        private int _health;
        private int _aguility;
        private int _armor;

        public PlayerStatsDebug(int power = 0, int health = 0, int aguility = 0, int armor = 0)
        {
            _power = power;
            _health = health;
            _aguility = aguility;
            _armor = armor;
        }

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
