using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameOverUiController : MonoBehaviour
{
    public UnityEvent onClickToEnd;
    private Button restart;

    private void Start()
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

        restart = root.Q<Button>("btn_otro");

        if (restart == null)
        {
            Debug.LogError("⚠️ No se encontró el elemento 'btn_otro' en el UXML.");
        }
        else
        {
            restart.RegisterCallback<ClickEvent>(RestartOnclicked);
        }
        
        Debug.Log($"Finished to config GameOver");
    }

    private void RestartOnclicked(ClickEvent evt)
    {
        Debug.Log($"Click");
        onClickToEnd?.Invoke();
    }
}