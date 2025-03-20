using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecisionNotify : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Image background;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private AwaitableCompletionSource<bool> decisionSource;

    public event System.Action OnClosed; // 🔥 Evento para avisar al manager

    public Awaitable<bool> ShowDecision(string message, NotificationType type)
    {
        decisionSource = new AwaitableCompletionSource<bool>();
        messageText.text = message;
        SetBackgroundColor(type);
        yesButton.onClick.AddListener(() => SelectOption(true));
        noButton.onClick.AddListener(() => SelectOption(false));
        gameObject.SetActive(true); // Mostrar la notificación

        return decisionSource.Awaitable;
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

    private void SelectOption(bool choice)
    {
        decisionSource.SetResult(choice);
        OnClosed?.Invoke(); // 🔥 Avisar al manager antes de destruir
        Destroy(gameObject);
    }
}