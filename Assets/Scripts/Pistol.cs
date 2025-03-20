using UnityEngine;

public class Pistol : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float attackCooldown = 1f;
    public float attackRange = 5f;
    public LayerMask enemyLayer;
    public int bulletDamage = 20;

    private float nextAttackTime;
    private PlayerMediator mediator;
    private IAttacker attacker;

    public void Initialize(PlayerMediator mediator, IAttacker a)
    {
        this.mediator = mediator;
        UpdateStats(mediator.PlayerStats);
        attacker = a;
    }

    public void UpdateStats(PlayerStats stats)
    {
        attackCooldown = stats.AttackCooldown;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            GameObject target = FindClosestEnemy();
            if (target != null)
            {
                Shoot(target.transform.position);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    GameObject FindClosestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        if (enemies.Length == 0) return null;

        GameObject closest = enemies[0].gameObject;
        float minDistance = Vector2.Distance(transform.position, closest.transform.position);

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                closest = enemy.gameObject;
                minDistance = distance;
            }
        }

        return closest;
    }

    void Shoot(Vector2 targetPosition)
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        PlayerBullet bullet = bulletObj.GetComponent<PlayerBullet>();
        Vector2 direction = (targetPosition - (Vector2)firePoint.position).normalized;
        bullet.Initialize(direction, bulletDamage, attacker);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}