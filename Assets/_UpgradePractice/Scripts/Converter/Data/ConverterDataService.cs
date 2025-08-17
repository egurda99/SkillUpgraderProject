namespace _UpgradePractice.Scripts
{
    public sealed class ConverterDataService
    {
        private ConverterData _converterData;

        public ConverterDataService(ConverterData converterData)
        {
            _converterData = converterData;
        }

        public ConverterData ConverterData => _converterData;

        public void SetConverterData(ConverterData converterData)
        {
            _converterData = converterData;
        }
    }
}