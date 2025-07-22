using UnityEngine;

namespace _UpgradePractice.Scripts
{
    [CreateAssetMenu(fileName = "ExchangeRate", menuName = "SO/Converter")]
    public sealed class ResourceExchangeRate : ScriptableObject
    {
        [SerializeField] private ResourceType _inputType;
        [SerializeField] private int _inputAmount;

        [SerializeField] private ResourceType _outputType;
        [SerializeField] private int _outputAmount;

        public ResourceType InputType => _inputType;
        public int InputAmount => _inputAmount;
        public ResourceType OutputType => _outputType;
        public int OutputAmount => _outputAmount;
    }
}
