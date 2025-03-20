using UnityEngine;

public interface INotificationService
{
    void Notify(string message, NotificationType type);
    Awaitable<bool> ShowDecision(string message, NotificationType type);
}