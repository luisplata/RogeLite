public interface IPlayerConfigurationService
{
    void SetCharacterType(CharacterType type);
    CharacterType GetCharacterType();
    
    void SetStats(PlayerGlobalStats stats);
    PlayerGlobalStats GetStats();
}