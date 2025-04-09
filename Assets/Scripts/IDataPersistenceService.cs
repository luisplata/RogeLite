public interface IDataPersistenceService
{
    void Save<T>(string key, T data);
    T Load<T>(string key, T defaultValue);
    void Delete(string key);
}