using System.Collections.Generic;
using Bellseboss;
using UnityEngine;

public class LootFactory : MonoBehaviour, ILootFactory
{
    [SerializeField] private LootItem gold;
    private ILootService _lootService;

    private void Awake()
    {
        if (FindObjectsByType<LootFactory>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        ILootItemFactory lootItemFactory = new RandomLootItemFactory();
        _lootService = new LootService(lootItemFactory, gold);

        ServiceLocator.Instance.RegisterService<ILootFactory>(this);
        ServiceLocator.Instance.RegisterService(_lootService);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<ILootFactory>();
        ServiceLocator.Instance.UnregisterService<ILootService>();
    }

    public List<LootItemInstance> GenerateLoot(LootTable lootTable, float luckFactor)
    {
        return _lootService.GenerateLoot(lootTable, luckFactor);
    }

    public LootItemInstance CreateLootItem(LootItem lootItem)
    {
        ILootItemFactory lootItemFactory = new RandomLootItemFactory();
        return lootItemFactory.CreateLootItem(lootItem);
    }

    public LootItemInstance GenerateGold(float luckFactor)
    {
        LootItemInstance goldLoot = CreateLootItem(gold);
        goldLoot.stars = Mathf.CeilToInt(5 * luckFactor);
        goldLoot.itemName = "Gold";
        goldLoot.itemType = LootType.Gold;
        return goldLoot;
    }
}