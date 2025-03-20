using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notify : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Image background;
    private Button closeButton;

    private AwaitableCompletionSource closeCompletionSource;

    private float displayDuration = 5f;
    private float minDisplayTime = 0.5f;
    private bool canBeClosed = false;

    public event System.Action OnClosed; // 🔥 Evento para avisar al manager

    public void Setup(string message, NotificationType type, Button closeButtonn)
    {
        closeButton = closeButtonn;
        closeButton.onClick.AddListener(Close);
        messageText.text = message;
        SetBackgroundColor(type);

        closeCompletionSource = new AwaitableCompletionSource();
        canBeClosed = false;

        StartCoroutine(AllowCloseAfter(minDisplayTime));
        StartCoroutine(AutoCloseCoroutine());
    }

    private void SetBackgroundColor(NotificationType type)
    {
        switch (type)
        {
            case NotificationType.Normal:
                background.color = Color.gray;
                break;
            case NotificationType.Good:
                background.color = Color.green;
                break;
            case NotificationType.Bad:
                background.color = Color.red;
                break;
        }
    }

    private IEnumerator AllowCloseAfter(float time)
    {
        yield return new WaitForSeconds(time);
        canBeClosed = true;
    }

    private IEnumerator AutoCloseCoroutine()
    {
        yield return new WaitForSeconds(displayDuration);
        Close();
    }

    public void Close()
    {
        if (!canBeClosed) return;
        StopAllCoroutines();
        closeCompletionSource.SetResult(); // Marca la tarea como completada

        OnClosed?.Invoke(); // 🔥 Avisar al manager antes de destruir
        Destroy(gameObject);
    }

    public Awaitable AwaitClose()
    {
        return closeCompletionSource.Awaitable;
    }
}
