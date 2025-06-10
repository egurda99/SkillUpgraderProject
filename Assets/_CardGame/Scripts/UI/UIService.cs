using UnityEngine;

namespace UI
{
    public sealed class UIService : MonoBehaviour
    {
        [SerializeField] private HeroListView _bluePlayer;

        [SerializeField] private HeroListView _redPlayer;

        public HeroListView GetBluePlayerList()
        {
            return _bluePlayer;
        }

        public HeroListView GetRedPlayerList()
        {
            return _redPlayer;
        }
    }
}
