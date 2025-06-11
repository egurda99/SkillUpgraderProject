using Cysharp.Threading.Tasks;

namespace _CardGame.EventTasks
{
    public abstract class BaseTask
    {
        public abstract UniTask Run();
    }
}
