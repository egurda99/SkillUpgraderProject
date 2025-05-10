public sealed class HPView : StatViewBase
{
    public override void SetValue(string value)
    {
        ValueText.text = "HIT POINTS: " + value;
    }
}
