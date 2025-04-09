using System.Collections.Generic;
using Bellseboss.Items;
using Items.Config;
using Items.Runtime;
using UnityEngine;

namespace Items
{
    public class ItemBuilder : IItemBuilder
    {
        private int _stars;
        private List<BaseStatsOnItem> _stats = new();
        private LootItem _lootItem;

        public static LootItemInstance Create(LootItem lootItem)
        {
            int stars = DetermineStars();

            return new ItemBuilder()
                .SetLootItem(lootItem)
                .SetStars(stars)
                .GenerateStats()
                .Build();
        }

        private ItemBuilder SetLootItem(LootItem lootItem)
        {
            _lootItem = lootItem;
            return this;
        }

        private ItemBuilder SetStars(int stars)
        {
            _stars = stars;
            return this;
        }

        private ItemBuilder GenerateStats()
        {
            var data = _lootItem.Data;

            if (data.lootType == LootType.Equipable)
            {
                switch (data.equipmentSlot)
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
            else if (data.lootType == LootType.Consumable)
            {
                SetStats(StatType.Heal, 20 * _stars);
            }

            return this;
        }

        private void SetStats(StatType statType, float statValue)
        {
            _stats.Add(new BaseStatsOnItem(statType, statValue));
        }

        private LootItemInstance Build()
        {
            var instance = new LootItemInstance(_lootItem, _stars);
            instance.Data.baseStats = _stats;
            return instance;
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
}