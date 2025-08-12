using System;

namespace _CardGame
{
    public interface IEventBus
    {
        void Subscribe<T>(Action<T> handler, int priority = 0);
        void Unsubscribe<T>(Action<T> handler);
        void RaiseEvent<T>(T evt);
    }
}
