using Atomic.Contexts;
using UnityEngine;

public class GameContextInstaller : SceneContextInstallerBase
{
    [SerializeField] private PlayerInputController _playerInputController;

    [SerializeField] private KeyboardInput _keyboardInput;

    [SerializeField] private MouseInput _mouseInput;

    [SerializeField] private CameraBasedKeyboardInput _cameraBasedKeyboardInput;


    public override void Install(IContext context)
    {
        //context.AddKeyboardInput(_keyboardInput);
        context.AddCameraBasedKeyboardInput(_cameraBasedKeyboardInput);
        context.AddMouseInput(_mouseInput);


        context.AddSystem(_playerInputController);
    }
}
