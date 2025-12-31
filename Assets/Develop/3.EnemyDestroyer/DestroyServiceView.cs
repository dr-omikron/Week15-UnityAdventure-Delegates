using TMPro;
using UnityEngine;

namespace Develop._3.EnemyDestroyer
{
    public class DestroyServiceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _storageCounter;

        private DestroyService _destroyService;

        public void Initialize(DestroyService destroyService)
        {
            _destroyService = destroyService;
        }

        private void Update()
        {
            SetStorageCounterText();
        }

        private void SetStorageCounterText() => _storageCounter.text = "Destroy service storage registered: " + _destroyService.StorageCount;
    }
}
