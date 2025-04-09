using UnityEngine;
using System.Collections.Generic;

namespace Bellseboss
{
    public abstract class DroppedLoot
    {
        public string itemName { get; set; }
        public LootType itemType { get; set; }
        public int stars { get; set; }
        public EquipmentSlot Slot { get; protected set; }
        public Sprite itemSprite { get; protected set; }
        public List<BaseStatsOnItem> stats { get; set; }
        
        public int priceToBuy { get; set; }
        
        public int priceToSell { get; set; }

        protected DroppedLoot(string name, LootType type, int stars, EquipmentSlot slot = EquipmentSlot.NONE, Sprite sprite = null, List<BaseStatsOnItem> stats = null)
        {
            itemName = name;
            itemType = type;
            this.stars = stars;
            Slot = slot;
            itemSprite = sprite;
            this.stats = stats ?? new List<BaseStatsOnItem>();
        }

        public bool IsEquipable() => Slot != null;
    }
}
