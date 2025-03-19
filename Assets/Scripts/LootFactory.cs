using System.Collections.Generic;
using Bellseboss;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootFactory : MonoBehaviour, ILootFactory
{
    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<ILootFactory>(this);
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
                Item newItem = ItemBuilder.Create(entry.item);
                droppedItems.Add(newItem);
                drops++;
            }
        }

        var goldAmount = Mathf.CeilToInt(5 * luckFactor);
        droppedItems.Add(new Item("Gold", LootType.Gold, goldAmount));

        return droppedItems;
    }
}