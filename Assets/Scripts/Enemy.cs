using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, ILevelEnemy, IDamageable
{
    public int health = 50;
    public float moveSpeed = 2f;
    public int level;

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
}