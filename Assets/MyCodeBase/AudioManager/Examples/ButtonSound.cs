using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Example
{
    [RequireComponent(typeof(Button))]
    public sealed class ButtonSound : MonoBehaviour
    {
        [SerializeField] private string _clipName = "buttonClick";
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (AudioManager.Instance.TryGetAudioClipByName(_clipName, out AudioClip audioClip))
            {
                AudioManager.Instance.PlaySound(audioClip, AudioOutput.UI);
            }
        }
    }
}
