using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MainUIController : MonoBehaviour
{
    public UnityEvent onClickToStart;
    private Label levelLabel;
    private Button startButton;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("⚠️ No se encontró el componente UIDocument en el GameObject.");
            return;
        }

        var root = uiDocument.rootVisualElement;
        if (root == null)
        {
            Debug.LogError(
                "⚠️ rootVisualElement es null. Asegúrate de que el UIDocument está configurado correctamente.");
            return;
        }

        levelLabel = root.Q<Label>("levelLabel");
        startButton = root.Q<Button>("startButton");

        if (levelLabel == null)
            Debug.LogError("⚠️ No se encontró el elemento 'levelLabel' en el UXML.");
        if (startButton == null)
            Debug.LogError("⚠️ No se encontró el elemento 'startButton' en el UXML.");

        if (startButton != null)
        {
            startButton.clicked += OnStartGame;
        }
    }

    private void OnStartGame()
    {
        Debug.Log("¡Juego Iniciado!");
        onClickToStart?.Invoke();
    }
}