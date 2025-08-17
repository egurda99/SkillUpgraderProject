using System;
using System.Collections.Generic;

namespace _UpgradePractice.Scripts
{
    [Serializable]
    public sealed class ConverterData
    {
        private float _ñonvertTime = 5f;
        private int _inputZoneCapacity = 4;
        private int _outputZoneCapacity = 4;
        private List<ResourceExchangeRate> _exchangeRate;


        private List<ResourceItem> _inputList = new();
        private List<ResourceItem> _outputList = new();

        public float ÑonvertTime => _ñonvertTime;
        public int InputZoneCapacity => _inputZoneCapacity;
        public int OutputZoneCapacity => _outputZoneCapacity;

        public List<ResourceExchangeRate> ExchangeRate => _exchangeRate;

        public List<ResourceItem> InputList => _inputList;
        public List<ResourceItem> OutputList => _outputList;

        public ConverterData(List<ResourceExchangeRate> exchangeRate)
        {
            _exchangeRate = exchangeRate;
        }

        public void SetConvertationTime(float time)
        {
            _ñonvertTime = time;
        }

        public void SetInputZoneCapacity(int inputZoneCapacity)
        {
            _inputZoneCapacity = inputZoneCapacity;
        }

        public void SetOutputZoneCapacity(int outputZoneCapacity)
        {
            _outputZoneCapacity = outputZoneCapacity;
        }

        public void SetExchangeRate(ResourceExchangeRate[] exchangeRate)
        {
            _exchangeRate.Clear();
            _exchangeRate.AddRange(exchangeRate);
        }
    }
}
