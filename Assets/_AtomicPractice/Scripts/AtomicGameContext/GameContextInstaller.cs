using Atomic.Contexts;
using Atomic.Entities;
using UnityEngine;

public class GameContextInstaller : SceneContextInstallerBase
{
    private PlayerInputController _playerInputController;

    [SerializeField] private KeyboardInput _keyboardInput;

    [SerializeField] private MouseInput _mouseInput;
    private SceneEntity _sceneEntity;

    [Zenject.Inject]
    public void Construct(SceneEntity player)
    {
        _sceneEntity = player;
        _playerInputController = new PlayerInputController(_sceneEntity);
    }

    public override void Install(IContext context)
    {
        context.AddKeyboardInput(_keyboardInput);
        context.AddMouseInput(_mouseInput);


        context.AddSystem(_playerInputController);
    }
}
