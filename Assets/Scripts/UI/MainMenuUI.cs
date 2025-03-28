using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour, IUIScreen
{
    [SerializeField] private UIDocument uiDocument;
    private VisualElement root;
    private Button startButton, exitButton, equipmentButton, storeButton;
    private UIManager uiManager;

    private void Awake()
    {
        root = uiDocument.rootVisualElement;
        startButton = root.Q<Button>("Button_Play");
        exitButton = root.Q<Button>("Button_Exit");
        equipmentButton = root.Q<Button>("Button_Equipment");
        storeButton = root.Q<Button>("Button_Store");
    }

    public void Initialize(UIManager manager)
    {
        uiManager = manager;
        uiManager.RegisterScreen("MainMenu", this);
        startButton.clicked += () => SceneManager.LoadScene(1);
        exitButton.clicked += () => Application.Quit();
        equipmentButton.clicked += () => uiManager.ShowScreen("EquipmentMenu");
        storeButton.clicked += () => uiManager.ShowScreen("Store");
    }

    public void Show() => root.style.display = DisplayStyle.Flex;
    public void Hide() => root.style.display = DisplayStyle.None;
}