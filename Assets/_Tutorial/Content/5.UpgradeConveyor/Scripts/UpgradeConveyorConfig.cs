using MyCodeBase;
using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "Config «UpgradeConveyorConfig»",
        menuName = "Tutorial/New Config «UpgradeConveyorConfig»"
    )]
    public sealed class UpgradeConveyorConfig : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private Sprite _sprite;


        [SerializeField] [TextArea(2, 4)] private string _description;


        [SerializeField] private UpgradeConveyorPanelShower _upgradeConveyorPanelShower;

        [SerializeField] private Transform _targetPosition;
        [SerializeField] private Transform _panelContainer;

        [SerializeField] private PopupName _popupName;
        [SerializeField] private int _targetLevel;

        [SerializeField] private GameObject _placeTriggerPointPrefab;

        public GameObject PlaceTriggerPointPrefab => _placeTriggerPointPrefab;


        public UpgradeConveyorPanelShower UpgradeConveyorPanelShower => _upgradeConveyorPanelShower;

        public Transform TargetPosition => _targetPosition;

        public Transform PanelContainer => _panelContainer;

        public PopupName PopupName => _popupName;

        public int TargetLevel => _targetLevel;

        public Sprite Sprite => _sprite;

        public string Title => _title;

        public string Description => _description;
    }
}
