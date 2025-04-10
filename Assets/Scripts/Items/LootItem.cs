using System.Collections.Generic;
using UnityEngine;
namespace Items
{
    [CreateAssetMenu(fileName = "NewLootItem", menuName = "Loot System/Loot Item")]
    public class LootItem : ScriptableObject
    {
        public string UUID; // Unique identifier for this config
        public string ItemName;
        public LootType LootType;
        public EquipmentSlot EquipmentSlot;
        public Sprite Icon;
        public int BuyPrice;
        public int SellPrice;
        public List<BaseStatOnItem> BaseStats = new();
    }
}


namespace Items
{
    public enum LootType
    {
        Equipment,
        Material,
        Consumable
    }

    public enum EquipmentSlot
    {
        None,
        Head,
        Chest,
        Pants,
        Shoes,
        RightHand,
        LeftHand,
        TwoHanded
    }
}



namespace Items
{
    public enum StatType
    {
        Attack,
        Defense,
        MoveSpeed,
        AttackSpeed,
        Health
    }
}