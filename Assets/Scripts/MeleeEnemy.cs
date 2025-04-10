using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;

    void Update()
    {
        Move();
        Attack();
    }

    protected override void Move()
    {
        if (player == null)
        {
            return;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        if (player == null) return;
        if (Time.time >= nextAttackTime && Vector2.Distance(transform.position, player.position) < 1f)
        {
            IDamageable damageable = player.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage, this);
            }

            nextAttackTime = Time.time + attackCooldown;
        }
    }
}