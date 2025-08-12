using MyCodeBase;
using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "Config «Welcome»",
        menuName = "Tutorial/New Config «Welcome»"
    )]
    public sealed class WelcomeConfig : ScriptableObject
    {
        [SerializeField] private string _title;

        [SerializeField] [TextArea(2, 4)] private string _description;

        [SerializeField] private PopupName _popupName;

        public PopupName PopupName => _popupName;

        public string Title => _title;

        public string Description => _description;
    }
}
