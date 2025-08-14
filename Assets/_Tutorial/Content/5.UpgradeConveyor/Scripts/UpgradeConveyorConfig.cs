using _UpgradePractice.Scripts;
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

        [SerializeField] private PopupName _popupName;
        [SerializeField] private UpgradeConfig _upgradeConfig;

        [SerializeField] private int _targetLevel;

        [SerializeField] private GameObject _placeTriggerPointPrefab;

        public GameObject PlaceTriggerPointPrefab => _placeTriggerPointPrefab;

        public UpgradeConfig UpgradeConfig => _upgradeConfig;

        public UpgradeConveyorPanelShower UpgradeConveyorPanelShower => _upgradeConveyorPanelShower;

        public PopupName PopupName => _popupName;

        public int TargetLevel => _targetLevel;

        public Sprite Sprite => _sprite;

        public string Title => _title;

        public string Description => _description;
    }
}
