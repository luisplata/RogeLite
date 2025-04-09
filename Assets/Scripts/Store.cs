using System;
using System.Collections.Generic;
using Items;
using Items.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour, IUIScreen
{
    [SerializeField] private GameObject store;
    [SerializeField] private Button closeButton;
    [SerializeField] private ItemToBuyContainer containerPrefab;
    [SerializeField] private GameObject parentToContainer;
    [SerializeField] private TextMeshProUGUI totalGoldText;

    private List<ItemToBuyContainer> instantiates = new();
    private UIManager uiManager;

    public void Initialize(UIManager manager)
    {
        uiManager = manager;
        uiManager.RegisterScreen("Store", this);
        closeButton.onClick.AddListener(HideStore);

        UpdateGold(ServiceLocator.Instance.GetService<IGoldService>().GetGold());
    }

    private void OnEnable()
    {
        ServiceLocator.Instance.GetService<IGoldService>().OnGoldChanged += UpdateGold;
        ServiceLocator.Instance.GetService<IInventoryService>().OnInventoryChanged += OnInventoryChanged;
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<IGoldService>().OnGoldChanged -= UpdateGold;
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
        var items = ServiceLocator.Instance.GetService<IDataBaseService>().GetItems();
        Debug.Log($"Count of items {items.Count}");

        foreach (var item in items)
        {
            var itemToBuy = Instantiate(containerPrefab, parentToContainer.transform);
            itemToBuy.Configure(item);
            instantiates.Add(itemToBuy);
        }

        store.SetActive(true);
    }

    public void Hide()
    {
        store.SetActive(false);

        foreach (var container in instantiates)
        {
            Destroy(container.gameObject);
        }

        instantiates = new List<ItemToBuyContainer>();
    }

    private void ClearStoreItems()
    {
        foreach (var container in instantiates)
        {
            Destroy(container.gameObject);
        }

        instantiates.Clear();
    }
}