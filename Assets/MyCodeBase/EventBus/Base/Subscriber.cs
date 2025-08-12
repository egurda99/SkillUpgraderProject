using System;

namespace _CardGame
{
    public sealed class Subscriber<T>
    {
        public Action<T> Handler;
        public int Priority;

        public Subscriber(Action<T> handler, int priority)
        {
            Handler = handler;
            Priority = priority;
        }
    }
}
