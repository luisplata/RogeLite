using Items.Runtime;

namespace Items
{
    public interface IPlayerConfigurationService
    {
        void SetCharacterType(CharacterType type);
        CharacterType GetCharacterType();
    
        void EquipItem(LootItemInstance item);
        LootItemInstance GetEquippedItem(EquipmentSlot slot);
    
        void SetStats(PlayerGlobalStats stats);
        PlayerGlobalStats GetStats();
    }
}