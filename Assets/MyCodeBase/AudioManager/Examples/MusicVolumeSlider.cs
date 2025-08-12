using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public sealed class MusicVolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private AudioOutput _audioOutput = AudioOutput.Music;

        private bool _isInitialized;

        private void Awake()
        {
            _slider.minValue = AudioManagerStaticData.CHANNEL_VOLUME_MINIMUM; // -80f
            _slider.maxValue = AudioManagerStaticData.CHANNEL_VOLUME_MAXIMUM; // 0f

            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void Start()
        {
            float currentVolume = AudioManager.Instance.GetRawMixerVolume(_audioOutput);
            _slider.SetValueWithoutNotify(currentVolume);
            _isInitialized = true;
        }

        private void OnSliderValueChanged(float value)
        {
            if (!_isInitialized)
                return;

            AudioManager.Instance.SetVolume(_audioOutput, _slider.value);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
}
