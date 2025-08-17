namespace MyCodeBase
{
    public sealed class LevelButtonTest : StateButtonBase<LevelButtonState>
    {
        public void SetAvailable(bool isAvailable)
        {
            var state = isAvailable ? LevelButtonState.Available : LevelButtonState.Locked;
            SetState(state);
        }
    }
}
