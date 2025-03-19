using UnityEngine;

public class PlayerStats : MonoBehaviour, ILevelPlayer, IDamageable
{
    private PlayerMediator mediator;

    public float baseDamage = 10f;
    public float attackCooldown = 1f;
    public float moveSpeed = 5f;
    public int health = 100;
    public int gold;
    [SerializeField] private int level = 1;
    [SerializeField] private int exp;
    [SerializeField] private XPConfig xpConfig;
    private XPManager xpManager;

    public int ExpToNextLevel => GetXpForLevel(level + 1);


    public void Initialize(PlayerMediator mediator)
    {
        this.mediator = mediator;
        ApplyStats();
        xpManager = new XPManager(xpConfig);
        xpManager.OnLevelUp += HandleLevelUp;
    }

    private void HandleLevelUp(int newLevel)
    {
        Debug.Log($"¡Nivel {newLevel} alcanzado!");
        // Aquí podrías dar recompensas al jugador.
        ApplyStats();
    }

    public void ApplyStats()
    {
        mediator.OnStatsChanged();
    }

    public void IncreaseDamage(float amount)
    {
        baseDamage += amount;
        ApplyStats();
    }

    public void ReduceAttackCooldown(float factor)
    {
        attackCooldown *= factor;
        ApplyStats();
    }

    public void IncreaseSpeed(float amount)
    {
        moveSpeed += amount;
        ApplyStats();
    }

    public void Heal(int amount)
    {
        health += amount;
        ApplyStats();
    }

    public int Level => level;

    public void TakeDamage(int amount, IAttacker attacker)
    {
        Heal(-amount);
        Debug.Log($"Health {health}");
        if (health <= 0)
        {
            Debug.Log($"Game Over");
            mediator.GameOver();
            attacker.OnKill(this);
        }
    }

    public void AddExp(int amount)
    {
        exp += amount;
        CheckLevelUp();
        ApplyStats();
    }

    private void CheckLevelUp()
    {
        while (exp >= ExpToNextLevel && level < xpConfig.maxLevel)
        {
            exp -= ExpToNextLevel; // Restar la exp necesaria para el nivel actual
            level++; // Subir de nivel

            //OnLevelUp?.Invoke(level); // Disparar el evento de subida de nivel
            mediator.LevelUp(level);

            //Debug.Log($"🎉 Subiste al nivel {level}!");
        }
    }

    public int GetExp()
    {
        return exp;
    }

    public void AddGold(int amountOfGold)
    {
        gold += amountOfGold;
        ApplyStats();
    }

    private int GetXpForLevel(int targetLevel)
    {
        return Mathf.FloorToInt(xpConfig.baseXP * Mathf.Pow(xpConfig.xpFactor, targetLevel - 1));
    }

    public string GetFormattedStats()
    {
        return $"<b>Level:</b> {level}\n" +
               $"<b>EXP:</b> {exp}/{ExpToNextLevel}\n" +
               $"<b>Health:</b> {health}\n" +
               $"<b>Damage:</b> {baseDamage}\n" +
               $"<b>Attack Cooldown:</b> {attackCooldown:F2}s\n" +
               $"<b>Speed:</b> {moveSpeed}\n" +
               $"<b>Gold:</b> {gold}";
    }
}