using System.Collections.Generic;
using Bellseboss;
using UnityEngine;

public class ItemBuilder
{
    private string _name;
    private LootType _type;
    private int _stars;
    private Dictionary<string, float> _stats = new();

    public static Item Create(LootItem lootItem)
    {
        int stars = DetermineStars(); // Calculamos las estrellas
        return new ItemBuilder()
            .SetName(lootItem.itemName)
            .SetType(lootItem.lootType)
            .SetStars(stars)
            .GenerateStats()
            .Build();
    }

    public ItemBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public ItemBuilder SetType(LootType type)
    {
        _type = type;
        return this;
    }

    public ItemBuilder SetStars(int stars)
    {
        _stars = stars;
        return this;
    }

    public ItemBuilder GenerateStats()
    {
        if (_type == LootType.Equipable)
        {
            _stats["Attack"] = 10 * _stars;
            _stats["Defense"] = 5 * _stars;
        }
        else if (_type == LootType.Consumable)
        {
            _stats["Heal"] = 20 * _stars;
        }
        return this;
    }

    public Item Build()
    {
        return new Item(_name, _type, _stars) { Stats = _stats };
    }

    private static int DetermineStars()
    {
        float roll = Random.value;
        if (roll > 0.95f) return 5;
        if (roll > 0.80f) return 4;
        if (roll > 0.50f) return 3;
        if (roll > 0.20f) return 2;
        return 1;
    }
}