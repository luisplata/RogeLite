using UnityEngine;

public class UIInitializer : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private MainMenuUI mainMenu;
    [SerializeField] private EquipmentCanvasUi equipmentMenu;
    [SerializeField] private Store store;

    private void Start()
    {
        mainMenu.Initialize(uiManager);
        equipmentMenu.Initialize(uiManager);
        store.Initialize(uiManager);
        uiManager.ShowScreen("MainMenu");
    }
}