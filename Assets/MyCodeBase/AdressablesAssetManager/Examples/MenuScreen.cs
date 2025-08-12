using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AssetManager.Examples
{
    public sealed class MenuScreen : MonoBehaviour
    {
        [SerializeField] private Button startButton;

        [SerializeField] private Button exitButton;

        private ApplicationExiter applicationExiter;
        private GameLoader gameLoader;
        private MenuLoader menuLoader;

        [Inject]
        public void Construct(ApplicationExiter applicationFinisher, GameLoader gameLoader, MenuLoader menuLoader)
        {
            this.gameLoader = gameLoader;
            this.menuLoader = menuLoader;
            applicationExiter = applicationFinisher;
        }

        private void OnEnable()
        {
            startButton.onClick.AddListener(OnStartClicked);
            exitButton.onClick.AddListener(applicationExiter.ExitApp);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(OnStartClicked);
            exitButton.onClick.RemoveListener(applicationExiter.ExitApp);
        }

        private void OnStartClicked()
        {
            OnStartClickedAsync().Forget();
        }

        private async UniTask OnStartClickedAsync()
        {
            await gameLoader.LoadGameAsync();
            await menuLoader.UnloadMenuAsync();
        }
    }
}
