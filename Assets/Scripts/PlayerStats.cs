using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStats : MonoBehaviour, ILevelPlayer, IDamageable, IGameUiController
{
    private PlayerMediator mediator;

    [SerializeField] private float baseDamage = 10f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int health = 100;
    [SerializeField] private int gold;
    [SerializeField] private int level = 1;
    [SerializeField] private int exp;
    [SerializeField] private XPConfig xpConfig;
    [SerializeField] private float miningTime = 1f;
    private XPManager xpManager;
    [SerializeField] private float currentDamage;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float currentCooldown;


    private List<BaseStatsOnItem> statModifiers = new();

    public int ExpToNextLevel => GetXpForLevel(level + 1);


    public void Initialize(PlayerMediator mediator)
    {
        this.mediator = mediator;
        UpdateStats();
        ApplyStats();
        xpManager = new XPManager(xpConfig);
        xpManager.OnLevelUp += HandleLevelUp;
    }

    private void HandleLevelUp(int newLevel)
    {
        Debug.Log($"¡Nivel {newLevel} alcanzado!");
        ApplyStats();
    }

    public void ApplyStats()
    {
        mediator.OnStatsChanged();
        OnUpdate?.Invoke(this);
    }

    public void ApplyStat(BaseStatsOnItem stat)
    {
        if (statModifiers.Contains(stat))
        {
            statModifiers.Find(s => s == stat).statValue += stat.statValue;
        }
        else
        {
            statModifiers.Add(stat);
        }

        UpdateStats();
    }

    public void RemoveStat(BaseStatsOnItem stat)
    {
        if (statModifiers.Contains(stat))
        {
            statModifiers.Remove(stat);
        }

        UpdateStats();
    }

    private void UpdateStats()
    {
        float totalDamage = baseDamage;
        float totalSpeed = moveSpeed;
        float totalCooldown = attackCooldown;

        foreach (var stat in statModifiers)
        {
            switch (stat.statType)
            {
                case StatType.Attack:
                    totalDamage += stat.statValue;
                    break;
                case StatType.Speed:
                    totalSpeed += stat.statValue;
                    break;
                case StatType.CooldownReduction:
                    totalCooldown -= stat.statValue;
                    break;
                case StatType.Heal:
                    health += Mathf.CeilToInt(stat.statValue);
                    break;
                case StatType.AttackSpeed:
                    totalCooldown *= stat.statValue;
                    break;
            }
        }

        // Aplicamos los valores finales
        currentDamage = totalDamage;
        currentSpeed = totalSpeed;
        currentCooldown = Mathf.Max(0.1f, totalCooldown);

        ApplyStats();
    }

    public int Level => level;
    public float MoveSpeed => currentSpeed;
    public float AttackCooldown => currentCooldown;
    public bool IsDead => health <= 0;
    public int Health => health;
    public int Damage => Mathf.CeilToInt(currentDamage);
    public int Gold => gold;

    public void TakeDamage(int amount, IAttacker attacker)
    {
        health -= amount;
        ApplyStats();
        if (health <= 0)
        {
            //Debug.Log("Game Over");
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
            exp -= ExpToNextLevel;
            level++;
            mediator.LevelUp(level);
        }
    }

    public int GetExp() => exp;

    public void AddGold(int amount)
    {
        gold += amount;
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

    public void IncreaseAttackSpeed(float speedMultiplier)
    {
        attackCooldown *= speedMultiplier;
    }

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IGameUiController>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Instance.UnregisterService<IGameUiController>();
    }

    public event Action<PlayerStats> OnUpdate;

    public float GetTimeToMining()
    {
        return miningTime;
    }
}