using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnRadius = 10f;

    [SerializeField] private float minSpawnRate = 0.5f; // 0.5 segundos entre spawns (máximo 120 enemigos por minuto)
    [SerializeField] private float maxSpawnRate = 3f; // 3 segundos entre spawns (20 enemigos por minuto)

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
            ene.SetLevel(levelPlayer.Level); // Asigna el nivel del jugador al enemigo
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

        float level = levelPlayer.Level;
        return Mathf.Lerp(maxSpawnRate, minSpawnRate, level / 100f);
        //TODO implement a max level from player
    }
}