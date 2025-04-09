using System.Collections.Generic;
using Bellseboss;
using Items;
using Items.Runtime;
using UnityEngine;

public class CharacterBuilder : ICharacterBuilder
{
    private CharacterType _characterType;
    private Dictionary<EquipmentSlot, LootItemInstance> _equippedItems = new();
    private PlayerGlobalStats _stats = new();

    public static PlayerCharacter Create(PlayerConfigurationService configService)
    {
        return new CharacterBuilder()
            .SetCharacterType(configService.GetCharacterType())
            .SetEquippedItems(configService.GetEquippedItem())
            .SetStats(configService.GetStats())
            .Build();
    }

    public ICharacterBuilder SetCharacterType(CharacterType type)
    {
        _characterType = type;
        return this;
    }

    public ICharacterBuilder SetEquippedItems(Dictionary<EquipmentSlot, LootItemInstance> equippedItems)
    {
        _equippedItems = equippedItems;
        return this;
    }

    public ICharacterBuilder SetStats(PlayerGlobalStats stats)
    {
        _stats = stats;
        return this;
    }

    public PlayerCharacter Build()
    {
        GameObject characterObject = new GameObject("PlayerCharacter");
        PlayerCharacter character = characterObject.AddComponent<PlayerCharacter>();
        character.Initialize(_characterType, _equippedItems, _stats);
        return character;
    }
}