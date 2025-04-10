using System.Collections.Generic;
using Inventory;
using Items;
using Items.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentCanvasUiModal : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject contentItems;
    [SerializeField] private ItemInstanceUiModal itemTemplate;
    private EquipmentCanvasUi _canvas;
    private List<LootItemInstance> items = new();
    private List<ItemInstanceUiModal> itemsInstances = new();

    public void Initialize(EquipmentCanvasUi canvas)
    {
        _canvas = canvas;
    }

    public void Show(EquipmentSlot slot)
    {
        items = ServiceLocator.Instance.GetService<IInventoryService>().GetAllItems();
        foreach (var item in items)
        {
            if (item.LootItemConfig.LootType == LootType.Equipment && item.LootItemConfig.EquipmentSlot == slot)
            {
                var itemElement = Instantiate(itemTemplate, contentItems.transform);
                itemElement.Initialize(item, this);
                itemsInstances.Add(itemElement);
            }
        }

        root.SetActive(true);
    }

    private void Start()
    {
        closeButton.onClick.AddListener(() => { _canvas.CloseModal(); });
    }

    public void Hide()
    {
        root.SetActive(false);
    }
    public void EquipItem(LootItemInstance item)
    {
        _canvas.EquipItem(item);
    }
}
