using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnRadius = 10f;

    [SerializeField] private float minSpawnRate = 0.5f; // Spawn más rápido
    [SerializeField] private float maxSpawnRate = 3f;   // Spawn más lento
    [SerializeField] private int maxPlayerLevel = 100;  // Nivel máximo del jugador para el cálculo

    private float nextSpawnTime;
    private int currentEnemyCount;
    private ILevelPlayer levelPlayer;

    void Start()
    {
        levelPlayer = player.GetComponent<ILevelPlayer>();

        if (levelPlayer == null)
        {
            Debug.LogError("El Player no implementa ILevelPlayer");
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + GetSpawnInterval();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);

        var ene = enemy.GetComponent<ILevelEnemy>();
        if (ene != null)
        {
            ene.SetLevel(levelPlayer.Level); // Ajusta el nivel del enemigo al del jugador
        }

        var enem = enemy.GetComponent<Enemy>();
        enem.OnDeath += HandleEnemyDeath;
        enem.Configure(player);
        currentEnemyCount++;
    }

    void HandleEnemyDeath()
    {
        currentEnemyCount--;
    }

    Vector2 GetRandomSpawnPosition()
    {
        Vector2 randomOffset = Random.insideUnitCircle.normalized * spawnRadius;
        return (Vector2)player.position + randomOffset;
    }

    float GetSpawnInterval()
    {
        if (levelPlayer == null) return maxSpawnRate;

        float level = Mathf.Clamp(levelPlayer.Level, 1, maxPlayerLevel);
        float progression = Mathf.Clamp01(level / (float)maxPlayerLevel);

        return Mathf.Lerp(maxSpawnRate, minSpawnRate, progression);
    }
}
