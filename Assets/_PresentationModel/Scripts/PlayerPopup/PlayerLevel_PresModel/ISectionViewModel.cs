using System;

namespace Lessons.Architecture.PM
{
    public interface ISectionViewModel : IViewModel, IDisposable
    {
        void Show();
        void Hide();
    }
}
