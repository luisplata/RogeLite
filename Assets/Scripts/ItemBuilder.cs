using System.Collections.Generic;
using Bellseboss;
using UnityEngine;

public class ItemBuilder: IItemBuilder
{
    private string _name;
    private LootType _type;
    private Sprite _image;
    private int _stars;
    private EquipmentSlot? _slot;
    private List<BaseStatsOnItem> _stats = new();

    public static LootItemInstance Create(LootItem lootItem)
    {
        int stars = DetermineStars(); // Calculamos estrellas
        return new ItemBuilder()
            .SetName(lootItem.itemName)
            .SetType(lootItem.lootType)
            .SetStars(stars)
            .SetImage(lootItem.itemSprite)
            .SetSlot(lootItem.equipmentSlot) // Ahora los ítems pueden tener un slot de equipamiento
            .GenerateStats()
            .Build(lootItem);
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

    public ItemBuilder SetImage(Sprite image)
    {
        _image = image;
        return this;
    }

    public ItemBuilder SetStars(int stars)
    {
        _stars = stars;
        return this;
    }

    public ItemBuilder SetSlot(EquipmentSlot? slot)
    {
        _slot = slot;
        return this;
    }

    public ItemBuilder GenerateStats()
    {
        if (_type == LootType.Equipable)
        {
            switch (_slot)
            {
                case EquipmentSlot.LeftHandWeapon:
                case EquipmentSlot.RightHandWeapon:
                    SetStats(StatType.Attack, 10 * _stars);
                    SetStats(StatType.AttackSpeed, Random.Range(0.5f, 1.5f) * _stars);
                    break;

                case EquipmentSlot.TwoHandedWeapon:
                    SetStats(StatType.Attack, 20 * _stars);
                    SetStats(StatType.AttackSpeed, Random.Range(0.3f, 1.0f) * _stars);
                    break;

                case EquipmentSlot.Helmet:
                case EquipmentSlot.Chestplate:
                case EquipmentSlot.Pants:
                case EquipmentSlot.Shoes:
                    SetStats(StatType.Defense, 5 * _stars);
                    SetStats(StatType.Heal, 20 * _stars);
                    break;
            }
        }
        else if (_type == LootType.Consumable)
        {
            SetStats(StatType.Heal, 20 * _stars);
        }

        return this;
    }

    private void SetStats(StatType statType, float statValue)
    {
        _stats.Add(new BaseStatsOnItem(statType, statValue));
    }

    public LootItemInstance Build(LootItem lootItem)
    {
        return new LootItemInstance(lootItem, _stars) { stats = _stats };
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

public interface IItemBuilder
{
}