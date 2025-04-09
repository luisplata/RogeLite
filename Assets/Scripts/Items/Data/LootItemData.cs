﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Data
{
    [Serializable]
    public class LootItemData
    {
        public string UUID;
        public string itemName;
        public LootType lootType;
        public EquipmentSlot equipmentSlot;
        public Sprite itemSprite;
        public int priceToBuy;
        public int priceToSell;
        public List<BaseStatsOnItem> baseStats = new();
    }
}