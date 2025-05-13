namespace Lessons.Architecture.PM
{
    public interface IPopupViewModelFactory
    {
        IPopupViewModel Create(PopupName name);
    }
}
