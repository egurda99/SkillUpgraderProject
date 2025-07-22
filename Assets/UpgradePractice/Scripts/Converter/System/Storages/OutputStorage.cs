using System.Collections.Generic;
using System.Linq;

namespace _UpgradePractice.Scripts
{
    public sealed class OutputStorage
    {
        private readonly List<ResourceItem> _outputList;
        private readonly ConverterData _data;
        private readonly ConverterSystem _system;

        public OutputStorage(ConverterData data, ConverterSystem system)
        {
            _outputList = data.OutputList;
            _system = system;
            _data = data;
        }

        public bool TryTakeAllOutput(out List<ResourceItem> items)
        {
            if (_data.OutputList.Count == 0)
            {
                items = null;
                return false;
            }

            items = new List<ResourceItem>(_data.OutputList);
            _data.OutputList.Clear();

            _system.FireOnOutputChanged(0);
            return true;
        }

        public bool HasOutputSpace()
        {
            return _data.OutputList.Count < _data.OutputZoneCapacity;
        }


        public int GetTotalOutputCount()
        {
            return _data.OutputList.Sum(i => i.Amount);
        }
    }
}
