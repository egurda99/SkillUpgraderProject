using System;
using System.Collections.Generic;
using System.Linq;

public sealed class EventBus : IEventBus
{
    private readonly Dictionary<Type, List<object>> _handlers = new();

    public void Subscribe<T>(Action<T> handler, int priority = 0)
    {
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
        var type = typeof(T);
        if (_handlers.TryGetValue(type, out var list))
        {
            list.RemoveAll(s => ((Subscriber<T>)s).Handler == handler);
        }
    }

    public void RaiseEvent<T>(T evt)
    {
        var type = typeof(T);
        if (_handlers.TryGetValue(type, out var list))
        {
            foreach (var obj in list.Cast<Subscriber<T>>())
            {
                obj.Handler.Invoke(evt);
            }
        }
    }
}
