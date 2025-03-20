using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private Button startButton, exitButton;

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

        exitButton = root.Q<Button>("Button_Exit");
        startButton = root.Q<Button>("Button_Play");

        if (exitButton == null)
            Debug.LogError("⚠️ No se encontró el elemento 'Button_Exit' en el UXML.");
        if (startButton == null)
            Debug.LogError("⚠️ No se encontró el elemento 'Button_Play' en el UXML.");

        if (startButton != null)
        {
            startButton.clicked += OnStartGame;
        }

        if (exitButton != null)
        {
            exitButton.clicked += OnExitButton;
        }
    }

    private void OnExitButton()
    {
        Debug.Log("¡Juego terminado!");
    }

    private void OnStartGame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    private void OnDisable()
    {
        if (startButton != null)
        {
            startButton.clicked -= OnStartGame;
        }

        if (exitButton != null)
        {
            exitButton.clicked -= OnExitButton;
        }
    }
}