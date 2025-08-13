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

        [SerializeField] private Transform _targetPosition;
        [SerializeField] private Transform _panelContainer;

        public TakeFromConveyorPanelShower TakeFromConveyorPanelShower => _takeFromConveyorPanelShower;

        public Transform TargetPosition => _targetPosition;

        public Transform PanelContainer => _panelContainer;

        public Sprite Sprite => _sprite;

        public string Title => _title;

        public string Description => _description;
    }
}
