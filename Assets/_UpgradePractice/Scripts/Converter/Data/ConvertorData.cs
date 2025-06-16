using System;
using System.Collections.Generic;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    [Serializable]
    public sealed class ConverterData
    {
        [SerializeField] private float _ñonvertTime;
        [SerializeField] private int _inputZoneCapacity;
        [SerializeField] private int _outputZoneCapacity;
        [SerializeField] private ResourceExchangeRate[] _exchangeRate;


        private List<ResourceItem> _inputList = new();
        private List<ResourceItem> _outputList = new();

        public float ÑonvertTime => _ñonvertTime;
        public int InputZoneCapacity => _inputZoneCapacity;
        public int OutputZoneCapacity => _outputZoneCapacity;

        public ResourceExchangeRate[] ExchangeRate => _exchangeRate;

        public List<ResourceItem> InputList => _inputList;
        public List<ResourceItem> OutputList => _outputList;
    }
}
