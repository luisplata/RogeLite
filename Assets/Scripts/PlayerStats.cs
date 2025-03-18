using UnityEngine;

public class PlayerStats : MonoBehaviour, ILevelPlayer, IDamageable
{
    private PlayerMediator mediator;

    public float baseDamage = 10f;
    public float attackCooldown = 1f;
    public float moveSpeed = 5f;
    public int health = 100;
    [SerializeField] private int level = 1;

    public void Initialize(PlayerMediator mediator)
    {
        this.mediator = mediator;
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
        Debug.Log($"Health {health}");
        if (health <= 0)
        {
            Debug.Log($"Game Over");
            mediator.GameOver();
        }
    }

    public int Level => level;

    public void TakeDamage(int amount)
    {
        Heal(-amount);
    }
}