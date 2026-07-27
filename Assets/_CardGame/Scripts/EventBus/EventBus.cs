using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _CardGame
{
    public sealed class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<object>> _handlers = new();

        public void Subscribe<T>(Action<T> handler, int priority = 0)
        {
            if (handler == null)
                return;

            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
            {
                _handlers[type] = new List<object>();
            }

            var subscribers = _handlers[type];
            subscribers.Add(new Subscriber<T>(handler, priority));


            subscribers.Sort((a, b) =>
                ((Subscriber<T>)b).Priority.CompareTo(((Subscriber<T>)a).Priority));
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            if (handler == null)
                return;

            var type = typeof(T);
            if (_handlers.TryGetValue(type, out var list))
            {
                list.RemoveAll(s => ((Subscriber<T>)s).Handler == handler);
            }
        }

        public void RaiseEvent<T>(T evt)
        {
            var type = typeof(T);
            if (!_handlers.TryGetValue(type, out var list))
                return;

            var snapshot = list.Cast<Subscriber<T>>().ToArray();

            foreach (var subscriber in snapshot)
            {
                try
                {
                    subscriber.Handler.Invoke(evt);
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }
        }
    }
}
