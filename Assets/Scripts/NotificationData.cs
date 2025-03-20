public class NotificationData
{
    public string Message { get; }
    public NotificationType Type { get; }

    public NotificationData(string message, NotificationType type)
    {
        Message = message;
        Type = type;
    }
}
public enum NotificationType
{
    Normal,
    Good,
    Bad
}
