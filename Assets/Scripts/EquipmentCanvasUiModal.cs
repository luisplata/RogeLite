using System;
using System.Collections.Generic;
using Bellseboss;
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

    private void Start()
    {
        closeButton.onClick.AddListener(() => { _canvas.CloseModal(); });
    }

    public void Show(EquipmentSlot slot)
    {
        items = ServiceLocator.Instance.GetService<IDataBaseService>().GetItems();
        foreach (var item in items)
        {
            if (item.Data.lootType == LootType.Equipable && item.Data.equipmentSlot == slot)
            {
                var itemElement = Instantiate(itemTemplate, contentItems.transform);
                itemElement.Initialize(item, this);
                itemsInstances.Add(itemElement);
            }
        }

        root.SetActive(true);
    }

    public void Hide()
    {
        foreach (var item in itemsInstances)
        {
            Destroy(item.gameObject);
        }

        itemsInstances.Clear();
        root.SetActive(false);
    }

    public void EquipItem(LootItemInstance item)
    {
        _canvas.EquipItem(item);
    }
}