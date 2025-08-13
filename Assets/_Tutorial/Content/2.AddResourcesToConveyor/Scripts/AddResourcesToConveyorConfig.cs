using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "Config «AddResourcesToConveyorConfig»",
        menuName = "Tutorial/New Config «AddResourcesToConveyorConfig»"
    )]
    public sealed class AddResourcesToConveyorConfig : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private Sprite _sprite;


        [SerializeField] [TextArea(2, 4)] private string _description;


        [SerializeField] private MoveToConveyorPanelShower _moveToConveyorPanelShower;

        [SerializeField] private Transform _targetPosition;
        [SerializeField] private Transform _panelContainer;

        public MoveToConveyorPanelShower MoveToConveyorPanelShower => _moveToConveyorPanelShower;

        public Transform TargetPosition => _targetPosition;

        public Transform PanelContainer => _panelContainer;

        public Sprite Sprite => _sprite;

        public string Title => _title;

        public string Description => _description;
    }
}
