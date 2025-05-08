using Atomic.Contexts;
using UnityEngine;

public class GameContextInstaller : SceneContextInstallerBase
{
    [SerializeField] private PlayerInputController _playerInputController;

    [SerializeField] private KeyboardInput _keyboardInput;

    public override void Install(IContext context)
    {
        context.AddKeyboardInput(_keyboardInput);

        context.AddSystem(_playerInputController);
    }
}
