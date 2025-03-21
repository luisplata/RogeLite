using UnityEngine;

public class DetectingPlayerFromEnemy : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enemy.PlayerIsConfigured() && other.CompareTag("Player"))
        {
            enemy.Configure(other.transform);
        }
    }
}