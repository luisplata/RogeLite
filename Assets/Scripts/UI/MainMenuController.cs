using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Store store;
    private Button startButton, exitButton, storeButton, editPlayerButton;
    [SerializeField] private UIDocument mainMenu, equipmentMenu;
    private VisualElement mainMenuRoot, equipmentMenuRoot;

    private void OnEnable()
    {
        
        if(equipmentMenu == null)
        {
            Debug.LogError("⚠️ No se encontró el componente UIDocument en el GameObject.");
            return;
        }
        
        equipmentMenuRoot = equipmentMenu.rootVisualElement;
        
        if (equipmentMenuRoot == null)
        {
            Debug.LogError("rootVisualElement es null. Asegúrate de que el UIDocument está configurado correctamente.");
            return;
        }
        
        if (mainMenu == null)
        {
            Debug.LogError("⚠️ No se encontró el componente UIDocument en el GameObject.");
            return;
        }

        mainMenuRoot = mainMenu.rootVisualElement;
        if (mainMenuRoot == null)
        {
            Debug.LogError("rootVisualElement es null. Asegúrate de que el UIDocument está configurado correctamente.");
            return;
        }

        exitButton = mainMenuRoot.Q<Button>("Button_Exit");
        startButton = mainMenuRoot.Q<Button>("Button_Play");
        storeButton = mainMenuRoot.Q<Button>("Button_Store");
        editPlayerButton = mainMenuRoot.Q<Button>("Button_Player");

        if (exitButton == null)
            Debug.LogError("⚠️ No se encontró el elemento 'Button_Exit' en el UXML.");
        if (startButton == null)
            Debug.LogError("⚠️ No se encontró el elemento 'Button_Play' en el UXML.");
        if (storeButton == null)
            Debug.LogError("⚠️ No se encontró el elemento 'Button_Store' en el UXML.");
        if (editPlayerButton == null)
            Debug.LogError("⚠️ No se encontró el elemento 'Button_Player' en el UXML.");

        if (startButton != null)
        {
            startButton.clicked += OnStartGame;
        }

        if (exitButton != null)
        {
            exitButton.clicked += OnExitButton;
        }

        if (storeButton != null)
        {
            storeButton.clicked += StoreButtonOnclicked;
        }
    }

    private void StoreButtonOnclicked()
    {
        store.ShowStore();
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