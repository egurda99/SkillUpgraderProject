using UnityEngine;
using UnityEngine.UI;

namespace MyCodeBase
{
    [RequireComponent(typeof(Button))]
    public sealed class PopupCloseButton : MonoBehaviour
    {
        [SerializeField] private Popup _popup;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnCloseClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnCloseClick);
        }


        private void OnCloseClick()
        {
            _popup.HideRequested();
        }
    }
}
