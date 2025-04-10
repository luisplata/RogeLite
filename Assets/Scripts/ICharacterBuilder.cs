public interface ICharacterBuilder
{
    ICharacterBuilder SetCharacterType(CharacterType type);
    ICharacterBuilder SetStats(PlayerGlobalStats stats);
    PlayerCharacter Build();
}