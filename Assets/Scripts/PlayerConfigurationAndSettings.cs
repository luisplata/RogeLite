using UnityEngine;

public class PlayerConfigurationAndSettings : MonoBehaviour
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
        _isConfigured = true;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (!_isConfigured) return;
        ServiceLocator.Instance.UnregisterService<IPlayerConfigurationService>();
    }
}