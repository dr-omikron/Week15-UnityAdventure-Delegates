using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Develop._3.EnemyDestroyer
{
    public class DestroyService
    {
        private readonly MonoBehaviour _coroutineRunner;
        private readonly int _maxStorageCount;

        private readonly List<IDestroyable> _destroyableStorage = new List<IDestroyable>();
        private readonly List<Coroutine> _processes = new List<Coroutine>();

        public DestroyService(MonoBehaviour coroutineRunner, int maxStorageCount)
        {
            _coroutineRunner = coroutineRunner;
            _maxStorageCount = maxStorageCount;
        }

        public int StorageCount => _destroyableStorage.Count;

        public void AddToStorage(IDestroyable destroyable)
        {
            if (_destroyableStorage.Contains(destroyable))
                return;

            _destroyableStorage.Add(destroyable);

            foreach (IDestroyable stored in _destroyableStorage)
                stored.AddCounter(1, _maxStorageCount);

            Coroutine process = _coroutineRunner.StartCoroutine(CheckDestroyProcess(destroyable, OnDestroyProcessEnd));
            _processes.Add(process);
        }

        private IEnumerator CheckDestroyProcess(IDestroyable destroyable, Action callback)
        {
            while(destroyable != null)
            {
                foreach (Func<bool> destroyCondition in destroyable.DestroyConditions.ToList())
                {
                    yield return new WaitUntil(destroyCondition.Invoke);

                    if (destroyable != null)
                    {
                        destroyable.Destroy();
                        _destroyableStorage.Remove(destroyable);

                        if(destroyable.Counter != _maxStorageCount)
                            foreach (IDestroyable stored in _destroyableStorage)
                                stored.AddCounter(-1, _maxStorageCount);
                    }

                    destroyable = null;
                }

                yield return null;
            }

            callback?.Invoke();
        }

        private void OnDestroyProcessEnd()
        {
            int count = _processes.Count;

            for (int i = 0; i < count; i++)
            {
                if (_processes[i] == null)
                    _processes.RemoveAt(i);
            }
        }
    }
}
