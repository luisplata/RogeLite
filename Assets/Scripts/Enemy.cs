using System;
using System.Collections.Generic;
using Inventory;
using Items;
using Items.Factories;
using Items.Runtime;
using LootSystem;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, ILevel, IDamageable, IXPSource, IAttacker, ILootable, IGoldLootable
{
    public int health = 50;
    public float moveSpeed = 2f;
    public int level;
    [SerializeField] private float luckFactor = 1.0f; //TODO get luck factor from player
    [SerializeField] private int xp = 1;
    [SerializeField] private LootTable lootTable;
    [SerializeField] private int goldBase = 1;

    public event Action OnDeath;

    protected Transform player;

    public void TakeDamage(int damage, IAttacker attacker)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(attacker);
        }
    }

    private void Die(IAttacker attacker)
    {
        OnDeath?.Invoke();
        attacker.OnKill(this);
        Destroy(gameObject);
    }

    protected abstract void Move();

    public void Configure(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void SetLevel(int levelFromPlayer)
    {
        level = levelFromPlayer;
    }

    public int GetXPAmount()
    {
        return xp * level;
    }

    public void OnKill(IDamageable target)
    {
        //Debug.Log($"Kill Player!");
    }

    public bool PlayerIsConfigured()
    {
        return player != null;
    }

    public List<LootItemInstance> GetLoot()
    {
        var lootItems = ServiceLocator.Instance.GetService<ILootFactory>()
            .GenerateLoot(lootTable, luckFactor);
        return lootItems;
    }

    public int GetGold()
    {
        return ServiceLocator.Instance.GetService<IGoldGenerationService>().GenerateGold(goldBase, luckFactor, level);
    }
}