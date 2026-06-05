using System.Collections.Generic;

namespace ServiceLocator
{
    public sealed class ServiceLocator
    {
        public static ServiceLocator Instance { get; private set; } = new();

        private readonly Dictionary<object, object> _services = new();

        public void Register<T>(object service)
        {
            _services.Add(typeof(T), service);
        }

        public void Register(object key, object service)
        {
            _services.Add(key, service);
        }

        public void Unregister<T>()
        {
            _services.Remove(typeof(T));
        }

        public void UnregisterAll()
        {
            _services.Clear();
        }

        public bool Contains<T>()
        {
            return _services.ContainsKey(typeof(T));
        }

        public T Get<T>()
        {
            return (T)_services[typeof(T)];
        }

        public T Get<T>(object key)
        {
            return (T)_services[key];
        }

        public List<T> GetAll<T>()
        {
            var result = new List<T>();
            foreach (var service in _services)
            {
                if (service.Value is T typedService)
                {
                    result.Add(typedService);
                }
            }

            return result;
        }
    }
}
