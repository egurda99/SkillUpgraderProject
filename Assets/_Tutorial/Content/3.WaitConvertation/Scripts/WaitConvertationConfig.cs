using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "Config «WaitConvertationConfig»",
        menuName = "Tutorial/New Config «WaitConvertationConfig»"
    )]
    public sealed class WaitConvertationConfig : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private Sprite _sprite;


        [SerializeField] [TextArea(2, 4)] private string _description;

        [SerializeField] private WaitConvertationPanelShower _waitConvertationPanelShower;

        [SerializeField] private Transform _panelContainer;

        public WaitConvertationPanelShower WaitConvertationPanelShower => _waitConvertationPanelShower;

        public Transform PanelContainer => _panelContainer;

        public Sprite Sprite => _sprite;

        public string Title => _title;

        public string Description => _description;
    }
}
