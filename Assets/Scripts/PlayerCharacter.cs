using System.Collections.Generic;
using Bellseboss;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterType CharacterType { get; private set; }
    public Dictionary<EquipmentSlot, LootItemInstance> EquippedItems { get; private set; }
    public PlayerGlobalStats Stats { get; private set; }

    public void Initialize(CharacterType type, Dictionary<EquipmentSlot, LootItemInstance> equippedItems, PlayerGlobalStats stats)
    {
        CharacterType = type;
        EquippedItems = equippedItems;
        Stats = stats;

        Debug.Log($"Character initialized: {CharacterType}");
        foreach (var item in EquippedItems)
        {
            Debug.Log($"Equipped {item.Key}: {item.Value.itemName}");
        }
    }
}