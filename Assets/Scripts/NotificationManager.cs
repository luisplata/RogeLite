using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private Notify notifyPrefab;
    [SerializeField] private DecisionNotify decisionNotifyPrefab;
    [SerializeField] private GameObject notificationParent;

    private Queue<(string, NotificationType)> notificationQueue = new();
    private bool isDisplaying = false;
    private Button buttonToClose;
    private CanvasGroup canvasGroup;
    private int activeNotifications = 0; // 🔥 Contador de notificaciones activas

    private void Start()
    {
        buttonToClose = notificationParent.GetComponent<Button>();
        canvasGroup = notificationParent.GetComponent<CanvasGroup>();
        HideParent();
    }

    private void HideParent()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void ShowParent()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void EnqueueNotification(string message, NotificationType type)
    {
        notificationQueue.Enqueue((message, type));
        if (!isDisplaying) _ = ShowNextNotification();
    }

    public Awaitable<bool> ShowDecision(string message, NotificationType type)
    {
        DecisionNotify decisionNotify = Instantiate(decisionNotifyPrefab, notificationParent.transform);
        ShowParent();
        activeNotifications++; // 🔥 Aumentar el contador
        decisionNotify.OnClosed += CheckAndHideParent; // 🔥 Suscribirse para saber cuándo se cierra

        return decisionNotify.ShowDecision(message, type);
    }

    private async Awaitable ShowNextNotification()
    {
        if (notificationQueue.Count == 0)
        {
            isDisplaying = false;
            CheckAndHideParent(); // 🔥 Ocultar si no hay más notificaciones
            return;
        }

        await Awaitable.WaitForSecondsAsync(0.1f); // 🔥 Esperar 0.1 segundos antes de mostrar la siguiente notificación

        ShowParent();
        isDisplaying = true;
        var (message, type) = notificationQueue.Dequeue();

        Notify notify = Instantiate(notifyPrefab, notificationParent.transform);
        notify.Setup(message, type, buttonToClose);
        activeNotifications++; // 🔥 Aumentar el contador
        notify.OnClosed += CheckAndHideParent; // 🔥 Suscribirse para saber cuándo se cierra

        await notify.AwaitClose();
        _ = ShowNextNotification(); // 🔥 Llamar a la siguiente notificación en la cola
    }


    private void CheckAndHideParent()
    {
        activeNotifications--; // 🔥 Reducir el contador
        if (activeNotifications <= 0)
        {
            HideParent();
        }
    }
}