using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] private GameObject sotre;
    [SerializeField] private Button closeButton;
    [SerializeField] private ItemToBuyContainer containerPrefab;
    [SerializeField] private GameObject parentToContainer;

    private List<ItemToBuyContainer> instantiates = new();

    private void Start()
    {
        closeButton.onClick.AddListener(HideStore);
        HideStore();
    }

    public void ShowStore()
    {
        var items = ServiceLocator.Instance.GetService<IDataBaseService>().GetItems();
        Debug.Log($"Count of items {items.Count}");
        foreach (var item in items)
        {
            var itemToBuy = Instantiate(containerPrefab, parentToContainer.transform);
            itemToBuy.Configure(item);
            instantiates.Add(itemToBuy);
        }

        sotre.SetActive(true);
    }

    public void HideStore()
    {
        sotre.SetActive(false);
        foreach (var container in instantiates)
        {
            Destroy(container.gameObject);
        }

        instantiates = new List<ItemToBuyContainer>();
    }
}