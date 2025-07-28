namespace Lessons.Architecture.PM
{
    public sealed class UserInfoSectionViewModel : ISectionViewModel
    {
        private readonly UserInfo _model;
        private readonly UserInfoAdapter _adapter;

        public UserInfoSectionViewModel(UserInfo model, UserInfoView view)
        {
            _model = model;
            _adapter = new UserInfoAdapter(_model, view);
        }

        public void Show()
        {
            _adapter.Show();
        }

        public void Hide()
        {
            _adapter.Hide();
        }

        public void Dispose()
        {
            _adapter.Dispose();
        }
    }
}
