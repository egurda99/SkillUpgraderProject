using System;
using System.Collections.Generic;
using System.Linq;
using MyTimer;

namespace _UpgradePractice.Scripts
{
    public sealed class ConverterSystem : IDisposable
    {
        private readonly ConverterData _data;
        private readonly InputStorage _inputStorage;
        private readonly OutputStorage _outputStorage;
        private readonly ConvertationMechanic _mechanic;

        public event Action<float> OnConvertProgressChanged;
        public event Action OnConvertCompleted;
        public event Action OnConvertStarted;

        public event Action<int> OnInputChanged;
        public event Action<int> OnOutputChanged;

        public ConverterSystem(ConverterData data, Timer timer)
        {
            _data = data;
            _inputStorage = new InputStorage(_data, this);
            _outputStorage = new OutputStorage(_data, this);

            var timerController = new ConvertTimerController(timer, _data);
            _mechanic = new ConvertationMechanic(_data, timerController);

            _mechanic.SetConditions(
                () => _inputStorage.HasEnoughInput(),
                () => _outputStorage.HasOutputSpace()
            );

            _mechanic.OnStarted += OnMechanicOnOnStarted;
            _mechanic.OnCompleted += ProcessConvert;
            _mechanic.OnProgress += OnMechanicOnOnProgress;
        }

        private void OnMechanicOnOnProgress(float value)
        {
            OnConvertProgressChanged?.Invoke(value);
        }


        public bool TryAddInput(ResourceItem item)
        {
            return _inputStorage.TryAdd(item);
        }

        public bool TryTakeAllOutput(out List<ResourceItem> items)
        {
            return _outputStorage.TryTakeAllOutput(out items);
        }

        public bool HasInputSpace()
        {
            return _inputStorage.GetTotalInputCount() < _data.InputZoneCapacity;
        }

        public void FireOnInputChanged(int input)
        {
            OnInputChanged?.Invoke(input);
        }

        public void FireOnOutputChanged(int output)
        {
            OnOutputChanged?.Invoke(output);
        }

        private void OnMechanicOnOnStarted()
        {
            OnConvertStarted?.Invoke();
        }

        public void OnUpdate()
        {
            _mechanic.Tick();
        }

        private void ProcessConvert()
        {
            foreach (var rate in _data.ExchangeRate)
            {
                if (!_inputStorage.HasEnoughForConvert(rate))
                    continue;

                _inputStorage.TakeInput(rate.InputType, rate.InputAmount);

                var existing = _data.OutputList.FirstOrDefault(i => i.Type == rate.OutputType);
                if (existing != null)
                    existing.Amount += rate.OutputAmount;
                else
                    _data.OutputList.Add(new ResourceItem(rate.OutputType, rate.OutputAmount));

                OnInputChanged?.Invoke(_inputStorage.GetTotalInputCount());
                OnOutputChanged?.Invoke(_outputStorage.GetTotalOutputCount());
                break;
            }

            OnConvertCompleted?.Invoke();
        }


        public void Dispose()
        {
            _mechanic.OnStarted -= OnMechanicOnOnStarted;
            _mechanic.OnCompleted -= ProcessConvert;
            _mechanic.OnProgress -= OnMechanicOnOnProgress;
            _mechanic.Dispose();
        }
    }
}
