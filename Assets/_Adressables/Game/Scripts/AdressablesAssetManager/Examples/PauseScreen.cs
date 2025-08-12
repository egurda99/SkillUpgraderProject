using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AssetManager.Examples
{
    public sealed class PauseScreen : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;

        [SerializeField] private Button exitButton;

        private MenuLoader menuLoader;
        private GameLoader gameLoader;

        [Inject]
        public void Construct(MenuLoader menuLoader, GameLoader gameLoader)
        {
            this.menuLoader = menuLoader;
            this.gameLoader = gameLoader;
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            resumeButton.onClick.AddListener(Hide);
            exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private async void OnExitButtonClick()
        {
            Time.timeScale = 1;
            await menuLoader.LoadMenuAsync();
            await gameLoader.UnloadGameAsync();
        }

        private void OnDisable()
        {
            resumeButton.onClick.RemoveListener(Hide);
            exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        public void Show()
        {
            Time.timeScale = 0; //KISS
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            Time.timeScale = 1; //KISS
            gameObject.SetActive(false);
        }
    }
}
