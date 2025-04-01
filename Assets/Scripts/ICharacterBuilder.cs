using System.Collections.Generic;
using Bellseboss;

public interface ICharacterBuilder
{
    ICharacterBuilder SetCharacterType(CharacterType type);
    ICharacterBuilder SetEquippedItems(Dictionary<EquipmentSlot, LootItemInstance> equippedItems);
    ICharacterBuilder SetStats(PlayerGlobalStats stats);
    PlayerCharacter Build();
}