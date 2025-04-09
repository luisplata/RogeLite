using System.Collections.Generic;
using Bellseboss;
using Items;
using Items.Runtime;

public interface ICharacterBuilder
{
    ICharacterBuilder SetCharacterType(CharacterType type);
    ICharacterBuilder SetEquippedItems(Dictionary<EquipmentSlot, LootItemInstance> equippedItems);
    ICharacterBuilder SetStats(PlayerGlobalStats stats);
    PlayerCharacter Build();
}