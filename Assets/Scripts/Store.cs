using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour, IUIScreen
{
    [SerializeField] private GameObject store;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject parentToContainer;
    [SerializeField] private TextMeshProUGUI totalGoldText;

    private UIManager uiManager;

    public void Initialize(UIManager manager)
    {
        uiManager = manager;
        uiManager.RegisterScreen("Store", this);
        closeButton.onClick.AddListener(HideStore);

    }

    private void OnInventoryChanged()
    {
        ClearStoreItems();
        ShowStore();
    }

    private void UpdateGold(int totalGold)
    {
        totalGoldText.text = $"Total gold is {totalGold}";
    }

    public void ShowStore()
    {
        Show();
    }

    public void HideStore()
    {
        uiManager.ShowScreen("MainMenu");
    }

    public void Show()
    {
        store.SetActive(true);
    }

    public void Hide()
    {
        store.SetActive(false);

    }

    private void ClearStoreItems()
    {
    }
}