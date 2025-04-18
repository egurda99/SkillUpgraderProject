using System;
using Zenject;

public sealed class ZenjectContext : IContext
{
    private DiContainer _container;

    public ZenjectContext(DiContainer container)
    {
        _container = container;
    }

    public void UpdateContainer(object container)
    {
        if (container is DiContainer di)
            _container = di;
        else
            throw new ArgumentException("Unsupported container type");
    }

    T IContext.GetService<T>()
    {
        return _container.Resolve<T>();
    }

    T[] IContext.GetServices<T>()
    {
        return _container.ResolveAll<T>().ToArray();
    }
}
