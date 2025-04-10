using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterType CharacterType { get; private set; }
    public PlayerGlobalStats Stats { get; private set; }

    public void Initialize(CharacterType type, PlayerGlobalStats stats)
    {
        CharacterType = type;
        Stats = stats;

        Debug.Log($"Character initialized: {CharacterType}");
    }
}