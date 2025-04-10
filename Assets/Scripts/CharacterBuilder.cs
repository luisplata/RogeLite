using UnityEngine;

public class CharacterBuilder : ICharacterBuilder
{
    private CharacterType _characterType;
    private PlayerGlobalStats _stats = new();

    public static PlayerCharacter Create(PlayerConfigurationService configService)
    {
        return new CharacterBuilder()
            .SetCharacterType(configService.GetCharacterType())
            .SetStats(configService.GetStats())
            .Build();
    }

    public ICharacterBuilder SetCharacterType(CharacterType type)
    {
        _characterType = type;
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
        character.Initialize(_characterType, _stats);
        return character;
    }
}