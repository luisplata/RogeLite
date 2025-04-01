using System;
using UnityEngine;

public class PlayerConfigurationAndSettings : MonoBehaviour, IPlayerConfigurationAndSettings
{
    private bool _isConfigured;

    private void Awake()
    {
        var count = FindObjectsByType<PlayerConfigurationAndSettings>(FindObjectsSortMode.None).Length;
        if (count > 1)
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Instance.RegisterService<IPlayerConfigurationAndSettings>(this);
        _isConfigured = true;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ServiceLocator.Instance.RegisterService<IPlayerConfigurationService>(new PlayerConfigurationService());
    }

    private void OnDestroy()
    {
        if (!_isConfigured) return;
        ServiceLocator.Instance.UnregisterService<IPlayerConfigurationAndSettings>();
        ServiceLocator.Instance.UnregisterService<IPlayerConfigurationService>();
    }
}