using System;

namespace _UpgradePractice.Scripts
{
    public sealed class InputZoneVisualAdapter : IDisposable
    {
        private readonly ConverterSystem _converterSystem;
        private readonly ZoneVisual _visualZone;

        public InputZoneVisualAdapter(ConverterSystem converterSystem, ZoneVisual visualZone)
        {
            _converterSystem = converterSystem;
            _visualZone = visualZone;

            _converterSystem.OnInputChanged += OnInputChanged;
        }

        private void OnInputChanged(int count)
        {
            _visualZone.SetupItems(count);
        }

        public void Dispose()
        {
            _converterSystem.OnInputChanged -= OnInputChanged;
        }
    }
}