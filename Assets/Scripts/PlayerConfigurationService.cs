using UnityEngine;

public class PlayerConfigurationService : IPlayerConfigurationService
{
    private const string CHARACTER_TYPE_KEY = "CharacterType";
    private const string EQUIPPED_ITEMS_KEY = "EquippedItems";
    private const string PLAYER_STATS_KEY = "PlayerStats";

    private CharacterType _characterType;
    private PlayerGlobalStats _stats = new();
    private readonly IDataPersistenceService _dataPersistenceService;
    private readonly IDataBaseService _dataBaseService;

    public PlayerConfigurationService(IDataPersistenceService dataPersistenceService,
        IDataBaseService dataBaseService)
    {
        _dataPersistenceService = dataPersistenceService;
        _dataBaseService = dataBaseService;
        LoadFromPrefs();
    }

    public void SetCharacterType(CharacterType type)
    {
        _characterType = type;
        PlayerPrefs.SetInt(CHARACTER_TYPE_KEY, (int)type);
        PlayerPrefs.Save();
    }

    public CharacterType GetCharacterType() => _characterType;


    public void SetStats(PlayerGlobalStats stats)
    {
        _stats = stats;
        SaveStats();
    }

    public PlayerGlobalStats GetStats() => _stats;

    private void LoadFromPrefs()
    {
        _characterType = (CharacterType)PlayerPrefs.GetInt(CHARACTER_TYPE_KEY, (int)CharacterType.Human);
        _stats = LoadStats();
    }

    private void SaveStats()
    {
        string statsJson = JsonUtility.ToJson(_stats);
        PlayerPrefs.SetString(PLAYER_STATS_KEY, statsJson);
        PlayerPrefs.Save();
    }

    private PlayerGlobalStats LoadStats()
    {
        string json = PlayerPrefs.GetString(PLAYER_STATS_KEY, "{}");
        return JsonUtility.FromJson<PlayerGlobalStats>(json) ?? new PlayerGlobalStats();
    }
}