using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public sealed class InputStorage
    {
        private readonly List<ResourceItem> _inputList;
        private readonly int _capacity;
        private readonly ConverterData _data;
        private readonly ConverterSystem _system;

        public InputStorage(ConverterData data, ConverterSystem system)
        {
            _inputList = data.InputList;
            _data = data;
            _system = system;
        }

        public int Total => _inputList.Sum(i => i.Amount);

        public bool HasSpace => Total < _data.InputZoneCapacity;

        public bool TryAdd(ResourceItem item)
        {
            if (item == null || item.Amount <= 0)
                return false;

            var remaining = item.Amount;
            var added = 0;

            while (remaining > 0 && GetTotalInputCount() < _data.InputZoneCapacity)
            {
                var existing = _data.InputList.FirstOrDefault(i => i.Type == item.Type);
                if (existing != null)
                {
                    existing.Amount += 1;
                }
                else
                {
                    _data.InputList.Add(new ResourceItem(item.Type, 1));
                }

                remaining--;
                added++;
            }

            if (added > 0)
            {
                Debug.Log($"<color=orange>Added item: {item.Type} amount: {added}</color>");
                _system.FireOnInputChanged(GetTotalInputCount());
                return true;
            }

            return false;
        }

        public int GetTotalInputCount()
        {
            return _data.InputList.Sum(i => i.Amount);
        }

        public void TakeInput(ResourceType type, int amount)
        {
            var remaining = amount;

            for (var i = 0; i < _data.InputList.Count && remaining > 0; i++)
            {
                var item = _data.InputList[i];
                if (item.Type != type)
                    continue;

                if (item.Amount <= remaining)
                {
                    remaining -= item.Amount;
                    _data.InputList.RemoveAt(i);
                    i--;
                }
                else
                {
                    item.Amount -= remaining;
                    remaining = 0;
                }
            }
        }

        public bool HasEnoughForConvert(ResourceExchangeRate rate)
        {
            var sum = 0;
            foreach (var r in _data.InputList)
            {
                if (r.Type == rate.InputType)
                    sum += r.Amount;
            }

            return sum >= rate.InputAmount;
        }

        public bool HasEnoughInput()
        {
            foreach (var rate in _data.ExchangeRate)
            {
                var total = _data.InputList
                    .Where(i => i.Type == rate.InputType)
                    .Sum(i => i.Amount);

                if (total >= rate.InputAmount)
                    return true;
            }

            return false;
        }
    }
}
