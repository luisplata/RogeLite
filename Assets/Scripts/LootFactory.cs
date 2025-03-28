using System;
using System.Collections.Generic;
using Bellseboss;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootFactory : MonoBehaviour, ILootFactory
{
    private void Awake()
    {
        var count = FindObjectsByType<LootFactory>(FindObjectsSortMode.None).Length;
        if (count > 1)
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Instance.RegisterService<ILootFactory>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<ILootFactory>();
    }

    public List<Item> GenerateLoot(LootTable lootTable, float luckFactor)
    {
        List<Item> droppedItems = new();
        int drops = 0;

        foreach (var entry in lootTable.lootEntries)
        {
            if (drops >= lootTable.maxDrops) break;
            float modifiedChance = entry.dropChance * luckFactor;
            if (Random.value <= modifiedChance)
            {
                var configItemInstantiate = Instantiate(entry.item);
                Item newItem = ItemBuilder.Create(configItemInstantiate);
                newItem.stats = configItemInstantiate.baseStatsOnItem;
                droppedItems.Add(newItem);
                drops++;
            }
        }

        // Agregar oro como mínimo
        var goldAmount = Mathf.CeilToInt(5 * luckFactor);
        droppedItems.Add(new Item("Gold", LootType.Gold, goldAmount));

        return droppedItems;
    }
}