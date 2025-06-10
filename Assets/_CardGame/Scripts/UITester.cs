using UI;
using UnityEngine;

namespace _CardGame
{
    public class UITester : MonoBehaviour
    {
        [SerializeField] private HeroListView _heroListView;


        private void OnEnable()
        {
            _heroListView.OnHeroClicked += DebugButton;
        }

        private void DebugButton(HeroView obj)
        {
            Debug.Log("Clickced");
            Debug.Log(obj.name);
        }

        private void OnDisable()
        {
            _heroListView.OnHeroClicked -= DebugButton;
        }
    }
}
