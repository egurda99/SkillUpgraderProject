public sealed class KillsView : StatViewBase
{
    public override void SetValue(string value)
    {
        ValueText.text = "Kills: " + value;
    }
}
