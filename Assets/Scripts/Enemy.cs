using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, ILevelEnemy, IDamageable
{
    public int health = 50;
    public float moveSpeed = 2f;
    public int level;
    [SerializeField] private LootTable lootTable;
    [SerializeField] private float luckFactor = 1.0f;

    public event Action OnDeath;

    protected Transform player;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();
        DropItems();
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

    public void DropItems()
    {
        var droppedItems = ServiceLocator.Instance.GetService<ILootFactory>().GenerateLoot(lootTable, luckFactor);

        foreach (var item in droppedItems)
        {
            Debug.Log($"Dropped: {item.Name} ({item.Type}, {item.Stars}⭐)");
            //TODO instantiate elements
        }
    }
}