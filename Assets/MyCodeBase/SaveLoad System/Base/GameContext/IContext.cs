public interface IContext
{
    T GetService<T>();
    T[] GetServices<T>();

    void UpdateContainer(object container);
}