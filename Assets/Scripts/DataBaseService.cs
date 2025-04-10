using UnityEngine;

public class DataBaseService : MonoBehaviour, IDataBaseService
{
    private IDataPersistenceService _dataPersistenceService;

    private void Awake()
    {
        if (FindObjectsByType<DataBaseService>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        _dataPersistenceService = new PlayerPrefsDataPersistenceService();

        ServiceLocator.Instance.RegisterService(_dataPersistenceService);
        ServiceLocator.Instance.RegisterService<IDataBaseService>(this);
        ServiceLocator.Instance.RegisterService<IPlayerConfigurationService>(
            new PlayerConfigurationService(_dataPersistenceService, this));

        DontDestroyOnLoad(gameObject);
    }
}