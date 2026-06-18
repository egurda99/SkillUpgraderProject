using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class GreetingPresenterZenject : MonoBehaviour
    {
        [SerializeField] private GreetingPassiveView _view;

        public void Show()
        {
            _view.SetMessage("Welcome to SkillUpgrader!");
        }

        public void Hide()
        {
        }
    }
}
