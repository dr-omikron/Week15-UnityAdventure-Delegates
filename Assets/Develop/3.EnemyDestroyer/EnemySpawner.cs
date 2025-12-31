using System.Collections.Generic;
using UnityEngine;

namespace Develop._3.EnemyDestroyer
{
    public class EnemySpawner
    {
        private readonly List<Enemy> _enemyPrefabs;
        private readonly Vector3 _spawnPosition;
        private readonly int _enemyHealth;
        private readonly float _spawnRadius;

        public EnemySpawner(List<Enemy> enemyPrefabs, int enemyHealth,  Vector3 spawnPosition, float spawnRadius)
        {
            _enemyPrefabs = enemyPrefabs;
            _enemyHealth = enemyHealth;
            _spawnPosition = spawnPosition;
            _spawnRadius = spawnRadius;
        }

        public Enemy Spawn()
        {
            int randomIndex = Random.Range(0, _enemyPrefabs.Count);
            Enemy enemyPrefab = _enemyPrefabs[randomIndex];

            Vector2 randomPosition = Random.insideUnitCircle * _spawnRadius;
            Enemy spawned = Object.Instantiate(enemyPrefab, new Vector3(randomPosition.x, 0, randomPosition.y) + _spawnPosition, Quaternion.identity);

            spawned.Initialize(_enemyHealth);

            return spawned;
        }
    }
}
