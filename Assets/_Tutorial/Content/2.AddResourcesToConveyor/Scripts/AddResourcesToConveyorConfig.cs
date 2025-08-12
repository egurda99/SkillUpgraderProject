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

        public Sprite Sprite => _sprite;

        public string Title => _title;

        public string Description => _description;
    }
}