namespace Modules.PopupsStandalone
{
    public abstract class PopupPresenter<T> : PopupPresenter where T : IPopupArgs
    {
        public override void Show(IPopupArgs args) => this.Show((T) args);

        public abstract void Show(T args);
    }
}
