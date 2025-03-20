using UnityEngine;

public class NotificationService : MonoBehaviour, INotificationService
{
    [SerializeField] private NotificationManager manager;
    private bool isProcessingNotification;

    private void Awake()
    {
        var count = FindObjectsByType<NotificationService>(FindObjectsSortMode.None).Length;
        if (count > 1)
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Instance.RegisterService<INotificationService>(this);
        DontDestroyOnLoad(gameObject);
    }

    public void Notify(string message, NotificationType type)
    {
        manager.EnqueueNotification(message, type);
    }

    public async Awaitable<bool> ShowDecision(string message, NotificationType type)
    {
        return await manager.ShowDecision(message, type);
    }
}