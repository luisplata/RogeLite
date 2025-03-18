using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMediator : MonoBehaviour
{
    public Player player;
    public PlayerStats playerStats;
    public Pistol pistol;
    public PowerUpManager powerUpManager;

    void Awake()
    {
        player = GetComponent<Player>();
        playerStats = GetComponent<PlayerStats>();
        pistol = GetComponentInChildren<Pistol>();
        powerUpManager = GetComponent<PowerUpManager>();

        // Configurar referencias en los componentes
        player.Initialize(this);
        playerStats.Initialize(this);
        pistol.Initialize(this);
        powerUpManager.Initialize(this);
    }

    public void OnPowerUpApplied(PowerUp powerUp)
    {
        Debug.Log($"Power-up aplicado: {powerUp.powerUpName}");
        powerUp.ApplyEffect(playerStats);
    }

    public void OnStatsChanged()
    {
        player.ApplyStats();
        pistol.UpdateStats(playerStats);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}