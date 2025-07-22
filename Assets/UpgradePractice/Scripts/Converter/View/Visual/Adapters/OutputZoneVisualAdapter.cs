using System;

namespace _UpgradePractice.Scripts
{
    public sealed class OutputZoneVisualAdapter : IDisposable
    {
        private readonly ConverterSystem _converterSystem;
        private readonly ZoneVisual _visualZone;

        public OutputZoneVisualAdapter(ConverterSystem converterSystem, ZoneVisual visualZone)
        {
            _converterSystem = converterSystem;
            _visualZone = visualZone;

            _converterSystem.OnOutputChanged += OnOutputChanged;
        }

        private void OnOutputChanged(int count)
        {
            _visualZone.SetupItems(count);
        }

        public void Dispose()
        {
            _converterSystem.OnInputChanged -= OnOutputChanged;
        }
    }
}
