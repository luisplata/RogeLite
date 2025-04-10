using System.Collections.Generic;
using Items.Runtime;

namespace Items.Equipment
{
    public interface IEquipmentPersistenceService
    {
        void SaveEquippedItems(List<LootItemInstance> equippedItems);
        List<LootItemInstance> LoadEquippedItems();
        LootItemInstance GetEquippedItem(EquipmentSlot slot);
        void EquipItem(LootItemInstance item);
    }
}