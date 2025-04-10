using System;
using System.Collections.Generic;
using Inventory;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour, IUIScreen
{
    [SerializeField] private GameObject store;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject parentToContainer;
    [SerializeField] private TextMeshProUGUI totalGoldText;
    [SerializeField] private ItemToBuyContainer itemToBuyContainerPrefab;
    private List<ItemToBuyContainer> itemToBuyContainers = new();

    private UIManager uiManager;

    public void Initialize(UIManager manager)
    {
        uiManager = manager;
        uiManager.RegisterScreen("Store", this);
        closeButton.onClick.AddListener(HideStore);
        UpdateGold(ServiceLocator.Instance.GetService<IPlayerGoldPersistenceService>().LoadGold());
    }

    private void OnEnable()
    {
        ServiceLocator.Instance.GetService<IPlayerGoldPersistenceService>().OnGoldChanged += UpdateGold;
        ServiceLocator.Instance.GetService<IInventoryService>().OnInventoryChanged += OnInventoryChanged;
    }

    private void OnDisable()
    {
        ServiceLocator.Instance.GetService<IPlayerGoldPersistenceService>().OnGoldChanged -= UpdateGold;
        ServiceLocator.Instance.GetService<IInventoryService>().OnInventoryChanged -= OnInventoryChanged;
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
        UpdateGold(ServiceLocator.Instance.GetService<IPlayerGoldPersistenceService>().LoadGold());
        var inventory = ServiceLocator.Instance.GetService<IInventoryService>().GetAllItems();
        foreach (var item in inventory)
        {
            var itemToBuyContainer = Instantiate(itemToBuyContainerPrefab, parentToContainer.transform);
            itemToBuyContainer.Configure(item);
            itemToBuyContainers.Add(itemToBuyContainer);
        }
    }

    public void Hide()
    {
        store.SetActive(false);
    }

    private void ClearStoreItems()
    {
        foreach (var itemToBuyContainer in itemToBuyContainers)
        {
            Destroy(itemToBuyContainer.gameObject);
        }

        itemToBuyContainers.Clear();
    }
}