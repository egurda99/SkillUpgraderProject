using System;
using System.Collections.Generic;
using System.Linq;
using MyTimer;

namespace _UpgradePractice.Scripts
{
    public sealed class ConverterSystem : IDisposable
    {
        private readonly ConverterData _data;
        private readonly ConverterView _view;

        private readonly CompositeCondition _canConvert = new();
        private readonly InputStorage _inputStorage;
        private readonly OutputStorage _outputStorage;
        private readonly ConvertTimerController _timerController;

        private bool _isConverting;
        private bool _isReadyToConvert;

        public event Action<float> OnConvertProgressChanged;
        public event Action OnConvertCompleted;
        public event Action OnConvertStarted;

        public event Action<int> OnInputChanged;
        public event Action<int> OnOutputChanged;

        public ConverterSystem(ConverterData data, Timer timer)
        {
            _data = data;
            _timerController = new ConvertTimerController(timer, _data);

            _timerController.OnElapsed += OnTimerEnded;
            _timerController.OnProgressChanged += OnTimerTick;

            _inputStorage = new InputStorage(_data, this);
            _outputStorage = new OutputStorage(_data, this);

            _canConvert.AppendCondition(() => _inputStorage.HasEnoughInput());
            _canConvert.AppendCondition(() => _outputStorage.HasOutputSpace());
        }

        public void OnUpdate()
        {
            if (!_isConverting)
                TryStartConversion();

            _timerController.Tick();

            if (_isReadyToConvert)
                CompleteConversion();
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

        private void TryStartConversion()
        {
            if (_canConvert.Invoke())
            {
                _timerController.UpdateConvertTime(_data.ÑonvertTime);
                _timerController.Start();

                _isConverting = true;
                _isReadyToConvert = false;
                OnConvertStarted?.Invoke();
            }
        }

        private void CompleteConversion()
        {
            TryConvert();
            OnConvertCompleted?.Invoke();
            _isConverting = false;
            _isReadyToConvert = false;
        }

        private void OnTimerEnded()
        {
            _isReadyToConvert = true;
        }

        private void OnTimerTick(float progress)
        {
            OnConvertProgressChanged?.Invoke(progress);
        }

        private void TryConvert()
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

                return;
            }
        }


        public void Dispose()
        {
            _timerController.Dispose();
        }
    }
}
