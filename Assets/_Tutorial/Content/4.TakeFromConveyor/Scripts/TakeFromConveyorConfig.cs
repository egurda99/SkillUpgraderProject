using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "Config «TakeFromConveyorConfig»",
        menuName = "Tutorial/New Config «TakeFromConveyorConfig»"
    )]
    public sealed class TakeFromConveyorConfig : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private Sprite _sprite;


        [SerializeField] [TextArea(2, 4)] private string _description;


        [SerializeField] private TakeFromConveyorPanelShower _takeFromConveyorPanelShower;


        public TakeFromConveyorPanelShower TakeFromConveyorPanelShower => _takeFromConveyorPanelShower;

        public Sprite Sprite => _sprite;

        public string Title => _title;

        public string Description => _description;
    }
}
