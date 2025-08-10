using Audio;
using UnityEngine;

namespace Example
{
    public sealed class PopupSoundComponent : MonoBehaviour
    {
        [SerializeField] private string _openClipName = "buttonClick";
        [SerializeField] private string _closeClipName = "buttonClick";

        public void PlayOpenSound()
        {
            if (AudioManager.Instance.TryGetAudioClipByName(_openClipName, out var audioClip))
            {
                AudioManager.Instance.PlaySound(audioClip, AudioOutput.UI);
            }
        }

        public void PlayCloseSound()
        {
            if (AudioManager.Instance.TryGetAudioClipByName(_closeClipName, out var audioClip))
            {
                AudioManager.Instance.PlaySound(audioClip, AudioOutput.UI);
            }
        }
    }
}
