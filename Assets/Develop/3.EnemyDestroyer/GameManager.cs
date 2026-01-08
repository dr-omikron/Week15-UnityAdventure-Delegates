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

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _raycaster = new Raycaster();

            _raycastSelector = new RaycastSelector(_raycaster, _playerInput, _selectionMask);
            _damager = new Damager(_playerInput, _raycastSelector, _damageAmount);

            DestroyService destroyService = new DestroyService(this, _maxStorageSize);
            _destroyServiceView.Initialize(destroyService);

            _enemySpawner = new EnemySpawner(
                _enemyPrefabs,
                _playerInput, 
                destroyService, 
                _raycastSelector, 
                this, 
                _enemyHealth, 
                _maxEnemyLifetime, 
                _spawnPoint.position, 
                _spawnRadius);
        }

        private void Update()
        {
            _playerInput.UpdateInput();
        }

        private void OnDestroy()
        {
            _raycastSelector.Deinitialize();
            _damager.Deinitialize();
            _enemySpawner.Deinitialize();
        }
    }
}
