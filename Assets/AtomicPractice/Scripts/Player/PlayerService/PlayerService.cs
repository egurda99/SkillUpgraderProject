using Atomic.Entities;

public sealed class PlayerService
{
    private SceneEntity _player;

    public SceneEntity Player => _player;

    public PlayerService(SceneEntity player)
    {
        _player = player;
    }

    public void SetPlayer(SceneEntity player)
    {
        _player = player;
    }
}