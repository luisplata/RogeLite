using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private int bulletDamage = 10;

    private float nextAttackTime;

    void Update()
    {
        Move();
        Attack();
    }

    protected override void Move()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        if (player == null) return;
        if (Time.time >= nextAttackTime && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Shoot();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();
        Vector2 direction = (player.position - firePoint.position).normalized;
        bullet.Initialize(direction, bulletDamage, this);
    }
}