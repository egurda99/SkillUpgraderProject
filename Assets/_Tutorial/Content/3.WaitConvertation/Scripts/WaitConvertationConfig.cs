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


        public WaitConvertationPanelShower WaitConvertationPanelShower => _waitConvertationPanelShower;


        public Sprite Sprite => _sprite;

        public string Title => _title;

        public string Description => _description;
    }
}
