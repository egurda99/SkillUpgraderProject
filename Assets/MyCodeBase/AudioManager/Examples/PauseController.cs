using Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace Example
{
    public sealed class PauseController : MonoBehaviour
    {
        private AudioMixerSnapshot _pauseSnapshot;
        private AudioMixerSnapshot _unPauseSnapshot;
        private bool _isPause;

        private void Start()
        {
            AudioManager.Instance.TryGetSnapshot(AudioManagerStaticData.PAUSE_SNAPSHOT_NAME, out _pauseSnapshot);
            AudioManager.Instance.TryGetSnapshot(AudioManagerStaticData.UNPAUSE_SNAPSHOT_NAME, out _unPauseSnapshot);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetPause();
            }
        }

        private void SetPause()
        {
            _isPause = !_isPause;
            AudioManager.Instance.Transition(_isPause ? _pauseSnapshot : _unPauseSnapshot);
        }
    }
}
