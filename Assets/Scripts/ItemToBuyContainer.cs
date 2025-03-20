using UnityEngine;

public class ItemToBuyContainer : MonoBehaviour
{
    public async void TryToBuyItem()
    {
        var response = await ServiceLocator.Instance.GetService<INotificationService>()
            .ShowDecision("Sure to buy?", NotificationType.Bad);
        if (response)
        {
            ServiceLocator.Instance.GetService<INotificationService>()
                .Notify("Buy!", NotificationType.Good);
        }
        else
        {
            ServiceLocator.Instance.GetService<INotificationService>()
                .Notify("Cancel Buy", NotificationType.Normal);
        }
    }
}