using UnityEngine;

public class PlayerPrefsDataPersistenceService : IDataPersistenceService
{
    public void Save<T>(string key, T data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
        Debug.Log($"Data saved {key} {json}");
    }

    public T Load<T>(string key, T defaultValue)
    {
        string json = PlayerPrefs.GetString(key, "{}");
        return string.IsNullOrEmpty(json) ? defaultValue : JsonUtility.FromJson<T>(json);
    }

    public void Delete(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }
}