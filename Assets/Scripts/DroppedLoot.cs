using UnityEngine;
using System.Collections.Generic;

namespace Bellseboss
{
    public abstract class DroppedLoot
    {
        public LootItemData Data { get; protected set; }
        public int stars { get; set; }

        protected DroppedLoot(LootItemData data, int stars)
        {
            Data = data;
            this.stars = stars;
        }

        public bool IsEquipable() => Data.equipmentSlot != EquipmentSlot.NONE;
    }
}