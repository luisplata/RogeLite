using System;
using Bellseboss;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, ILevelEnemy, IDamageable, IXPSource, IAttacker, ILootSource
{
    public int health = 50;
    public float moveSpeed = 2f;
    public int level;
    [SerializeField] private LootTable lootTable;
    [SerializeField] private float luckFactor = 1.0f;
    [SerializeField] private int xp = 1;

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

    protected virtual void Die(IAttacker attacker)
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

    public Item[] GetLoot()
    {
        return ServiceLocator.Instance.GetService<ILootFactory>().GenerateLoot(lootTable, luckFactor).ToArray();
    }

    public bool PlayerIsConfigured()
    {
        return player != null;
    }
}