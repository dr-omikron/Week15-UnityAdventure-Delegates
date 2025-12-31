using System.Collections.Generic;
using UnityEngine;

namespace Develop._3.EnemyDestroyer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemyPrefabs;
        [SerializeField] private LayerMask _selectionMask;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _spawnRadius;

        [SerializeField] private int _enemyHealth;
        [SerializeField] private float _maxEnemyLifetime;

        [SerializeField] private int _damageAmount;
        [SerializeField] private int _maxStorageSize;

        [SerializeField] private DestroyServiceView _destroyServiceView;

        private PlayerInput _playerInput;
        private Raycaster _raycaster;
        private RaycastSelector _raycastSelector;
        private Damager _damager;
        private EnemySpawner _enemySpawner;
        private DestroyService _destroyService;

        private readonly List<Enemy> _spawnedStorage = new List<Enemy>();

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _raycaster = new Raycaster();
            _raycastSelector = new RaycastSelector(_raycaster, _playerInput, _selectionMask);
            _damager = new Damager(_playerInput, _raycastSelector, _damageAmount);
            _enemySpawner = new EnemySpawner(_enemyPrefabs, _enemyHealth, _spawnPoint.position, _spawnRadius);
            _destroyService = new DestroyService(this, _maxStorageSize);
            _destroyServiceView.Initialize(_destroyService);

            _playerInput.AddToDeathList += OnAddToDeathList;
            _playerInput.AddToLifetimeList += OnAddToLifetimeList;
            _playerInput.AddToCountLimitList += OnAddToCountLimitList;
            _playerInput.Spawned += OnSpawned;
        }

        private void Update()
        {
            _playerInput.UpdateInput();
        }

        private void OnDestroy()
        {
            _playerInput.AddToDeathList -= OnAddToDeathList;
            _playerInput.AddToLifetimeList -= OnAddToLifetimeList;
            _playerInput.AddToCountLimitList -= OnAddToCountLimitList;
            _playerInput.Spawned -= OnSpawned;

            _raycastSelector.Deinitialize();
            _damager.Deinitialize();
        }

        private void OnSpawned() => _spawnedStorage.Add(_enemySpawner.Spawn());

        private void OnAddToCountLimitList()
        {
            ISelectable selectable = _raycastSelector.Selectable;

            if(selectable != null)
            {
                foreach (IDestroyable destroyable in _spawnedStorage)
                {
                    if (destroyable == selectable)
                    {
                        destroyable.AddDestroyCondition(() => destroyable.Counter >= _maxStorageSize);
                        _destroyService.AddToStorage(destroyable);
                    }
                }
            }
        }

        private void OnAddToLifetimeList()
        {
            ISelectable selectable = _raycastSelector.Selectable;

            if(selectable != null)
            {
                foreach (IDestroyable destroyable in _spawnedStorage)
                {
                    if (destroyable == selectable)
                    {
                        destroyable.AddDestroyCondition(() => destroyable.LifeTime > _maxEnemyLifetime);
                        destroyable.IsLifeTimeConditionActivated(destroyable.LifeTime, _maxEnemyLifetime);
                        _destroyService.AddToStorage(destroyable);
                    }
                }
            }
        }

        private void OnAddToDeathList()
        {
            ISelectable selectable = _raycastSelector.Selectable;

            if(selectable != null)
            {
                foreach (IDestroyable destroyable in _spawnedStorage)
                {
                    if (destroyable == selectable)
                    {
                        destroyable.AddDestroyCondition(() => destroyable.IsDead);
                        destroyable.IsDeadConditionActivated();
                        _destroyService.AddToStorage(destroyable);
                    }
                }
            }
        }
    }
}
