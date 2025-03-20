using System;
using ShootEmUp;
using UnityEngine;
using UnityEngine.UI;

public sealed class StartGameWidget : MonoBehaviour, IGameFinishListener
{
    [SerializeField] private Button _startGameButton;

    public event Action OnStartGameButtonPressed;

    private void OnEnable() => _startGameButton.onClick.AddListener(StartButtonClicked);

    public void ShowButton() => _startGameButton.gameObject.SetActive(true);

    public void HideButton() => _startGameButton.gameObject.SetActive(false);

    private void StartButtonClicked() => OnStartGameButtonPressed?.Invoke();

    void IGameFinishListener.OnFinishGame() => _startGameButton.onClick.RemoveListener(StartButtonClicked);
}
