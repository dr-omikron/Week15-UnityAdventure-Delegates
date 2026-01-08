using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Develop._3.EnemyDestroyer
{
    public class DestroyService
    {
        private readonly MonoBehaviour _coroutineRunner;

        private readonly Dictionary<IDestroyable, Func<bool>> _destroyableStorage = new Dictionary<IDestroyable, Func<bool>>();
        private readonly List<Coroutine> _processes = new List<Coroutine>();

        public DestroyService(MonoBehaviour coroutineRunner, int maxStorageCount)
        {
            _coroutineRunner = coroutineRunner;
            MaxStorageCount = maxStorageCount;
        }

        public int MaxStorageCount { get; }

        public int StorageCount => _destroyableStorage.Count;

        public void AddToStorage(IDestroyable destroyable, Func<bool> destroyReason)
        {
            if (_destroyableStorage.TryAdd(destroyable, destroyReason) == false)
                return;

            Coroutine process = _coroutineRunner.StartCoroutine(CheckDestroyProcess(destroyable, destroyReason, OnDestroyProcessEnd));
            _processes.Add(process);
        }

        private IEnumerator CheckDestroyProcess(IDestroyable destroyable, Func<bool> destroyReason, Action callback)
        {
            while(destroyable != null)
            {
                yield return new WaitUntil(destroyReason.Invoke);

                destroyable.Destroy();
                _destroyableStorage.Remove(destroyable);

                destroyable = null;

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
