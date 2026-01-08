using System.Collections.Generic;
using Develop._2.Timer;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Develop._3.EnemyDestroyer
{
    public class EnemySpawner
    {
        private readonly List<Enemy> _enemyPrefabs;
        private readonly PlayerInput _playerInput;
        private readonly Vector3 _spawnPosition;
        private readonly int _enemyHealth;
        private readonly float _enemyLifeTime;
        private readonly float _spawnRadius;
        private readonly DestroyService _destroyService;
        private readonly RaycastSelector _raycastSelector;
        private readonly List<Enemy> _spawnedStorage = new List<Enemy>();
        private readonly MonoBehaviour _coroutineRunner;

        public EnemySpawner(
            List<Enemy> enemyPrefabs, 
            PlayerInput playerInput, 
            DestroyService destroyService, 
            RaycastSelector raycastSelector, 
            MonoBehaviour coroutineRunner,
            int enemyHealth, 
            float enemyLifeTime, 
            Vector3 spawnPosition, 
            float spawnRadius)
        {
            _enemyPrefabs = enemyPrefabs;
            _playerInput = playerInput;
            _destroyService = destroyService;
            _raycastSelector = raycastSelector;
            _coroutineRunner = coroutineRunner;
            _enemyHealth = enemyHealth;
            _enemyLifeTime = enemyLifeTime;
            _spawnPosition = spawnPosition;
            _spawnRadius = spawnRadius;

            _playerInput.AddToDeathList += OnAddToDeathList;
            _playerInput.AddToLifetimeList += OnAddToLifetimeList;
            _playerInput.AddToCountLimitList += OnAddToCountLimitList;
            _playerInput.Spawned += OnSpawned;
        }

        public void Deinitialize()
        {
            _playerInput.AddToDeathList -= OnAddToDeathList;
            _playerInput.AddToLifetimeList -= OnAddToLifetimeList;
            _playerInput.AddToCountLimitList -= OnAddToCountLimitList;
            _playerInput.Spawned -= OnSpawned;
        }

        private void OnSpawned() => _spawnedStorage.Add(Spawn());

        private void OnAddToCountLimitList()
        {
            ISelectable selectable = _raycastSelector.Selectable;

            if(selectable != null)
                foreach (Enemy enemy in _spawnedStorage)
                    if ((Enemy)selectable == enemy)
                        _destroyService.AddToStorage(enemy, () => _destroyService.StorageCount > _destroyService.MaxStorageCount);
        }

        private void OnAddToLifetimeList()
        {
            ISelectable selectable = _raycastSelector.Selectable;

            if(selectable != null)
            {
                foreach (Enemy enemy in _spawnedStorage)
                {
                    if ((Enemy)selectable == enemy)
                    {
                        Timer timer = new Timer(_coroutineRunner);
                        timer.SetTime(_enemyLifeTime);
                        timer.StartProcess();

                        _destroyService.AddToStorage(enemy, () => timer.InProcess() == false);
                    }
                }
            }
        }

        private void OnAddToDeathList()
        {
            ISelectable selectable = _raycastSelector.Selectable;

            if(selectable != null)
                foreach (Enemy enemy in _spawnedStorage)
                    if ((Enemy)selectable == enemy)
                        _destroyService.AddToStorage(enemy, () => enemy.IsDead);
        }

        private Enemy Spawn()
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
