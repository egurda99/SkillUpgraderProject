using MyCodeBase;
using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "Config «FinalStepConfig»",
        menuName = "Tutorial/New Config «FinalStepConfig»"
    )]
    public sealed class FinalStepConfig : ScriptableObject
    {
        [Header("Panel")] [SerializeField] private string _title;
        [SerializeField] private Sprite _sprite;


        [SerializeField] [TextArea(2, 4)] private string _description;
        [Header("Popup")] [SerializeField] private string _titlePopup;
        [SerializeField] [TextArea(2, 4)] private string _descriptionPopup;

        public string TitlePopup => _titlePopup;

        public string DescriptionPopup => _descriptionPopup;

        [SerializeField] private FinalPanelShower _finalPanelShower;

        [SerializeField] private PopupName _popupName;


        [SerializeField] private GameObject _placeTriggerPointPrefab;

        public GameObject PlaceTriggerPointPrefab => _placeTriggerPointPrefab;


        public FinalPanelShower FinalPanelShower => _finalPanelShower;

        public PopupName PopupName => _popupName;


        public Sprite Sprite => _sprite;

        public string Title => _title;

        public string Description => _description;
    }
}
