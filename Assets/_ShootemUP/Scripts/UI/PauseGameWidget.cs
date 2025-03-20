using System;
using ShootEmUp;
using UnityEngine;
using UnityEngine.UI;

public sealed class PauseGameWidget : MonoBehaviour, IGameFinishListener
{
    [SerializeField] private Button _pauseGameButton;

    public event Action OnPauseGameButtonPressed;

    private void OnEnable() => _pauseGameButton.onClick.AddListener(PauseButtonClicked);

    public void ShowButton() => _pauseGameButton.gameObject.SetActive(true);

    public void HideButton() => _pauseGameButton.gameObject.SetActive(false);


    private void PauseButtonClicked() => OnPauseGameButtonPressed?.Invoke();

    void IGameFinishListener.OnFinishGame() => _pauseGameButton.onClick.RemoveListener(PauseButtonClicked);
}
