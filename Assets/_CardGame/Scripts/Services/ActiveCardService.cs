using UI;

namespace _CardGame.Services
{
    public sealed class ActiveCardService
    {
        public HeroView ActiveHeroView => _activeHeroView;

        private HeroView _activeHeroView;

        public void SetActiveHeroView(HeroView activeHeroView)
        {
            _activeHeroView = activeHeroView;
//            Debug.Log("[ActiveCardService] ActiveHeroName: " + _activeHeroView.name);
        }
    }
}
