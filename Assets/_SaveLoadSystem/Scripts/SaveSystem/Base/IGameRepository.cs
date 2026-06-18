public interface IGameRepository
{
    bool TryGetData<T>(string key, out T data);
    void SetData<T>(string key, T data);
}