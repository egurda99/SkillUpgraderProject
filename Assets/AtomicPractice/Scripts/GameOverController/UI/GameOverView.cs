using UnityEngine;

public sealed class GameOverView : MonoBehaviour
{
    [SerializeField] private GameObject _gaveOverView;

    public void Show()
    {
        _gaveOverView.SetActive(true);
    }

    public void Hide()
    {
        _gaveOverView.SetActive(false);
    }
}
