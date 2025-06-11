using UnityEngine;

namespace _CardGame.DI
{
    public sealed class InstallerHelper : MonoBehaviour
    {
        [SerializeField] private GameEndView _gameEndView;

        public GameEndView GameEndView => _gameEndView;
    }
}