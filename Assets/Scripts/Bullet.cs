using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifetime = 3f; // Tiempo antes de autodestruirse

    private Vector2 direction;
    private IAttacker attacker;

    public void Initialize(Vector2 direction, int damage, IAttacker a)
    {
        this.direction = direction.normalized;
        this.damage = damage;
        attacker = a;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += (Vector3)direction * (speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage, attacker);
            Destroy(gameObject);
        }
    }
}