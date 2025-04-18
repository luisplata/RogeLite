﻿using System;
using Inventory;
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

    public int ExpToNextLevel => GetXpForLevel(level + 1);


    public void Initialize(PlayerMediator mediator)
    {
        this.mediator = mediator;
        UpdateStats();
        ApplyStats();
        xpManager = new XPManager(xpConfig);
        xpManager.OnLevelUp += HandleLevelUp;
        gold = ServiceLocator.Instance.GetService<IPlayerGoldPersistenceService>().LoadGold();
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

    private void UpdateStats()
    {
        float totalDamage = baseDamage;
        float totalSpeed = moveSpeed;
        float totalCooldown = attackCooldown;

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

    public float GetLuckFactor()
    {
        // Implementar la lógica para calcular el factor de suerte
        // Por ahora, devolvemos un valor fijo
        return 1.0f;
    }
}