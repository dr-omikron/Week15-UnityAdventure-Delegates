using System;
using UnityEngine;

namespace Develop._3.EnemyDestroyer
{
    public class PlayerInput
    {
        public event Action AddToDeathList;
        public event Action AddToLifetimeList;
        public event Action AddToCountLimitList;
        public event Action DamageTaken;
        public event Action RayCasted;
        public event Action Spawned;

        private const KeyCode AddToDeathListKey = KeyCode.Alpha1;
        private const KeyCode AddToLifetimeListKey = KeyCode.Alpha2;
        private const KeyCode AddToCountLimitListKey = KeyCode.Alpha3;
        private const KeyCode DamageTakenKey = KeyCode.D;
        private const KeyCode RayCastedKey = KeyCode.Mouse0;
        private const KeyCode SpawnKey = KeyCode.F;

        public Vector3 MousePosition { get; private set; }

        public void UpdateInput()
        {
            if (Input.GetKeyDown(AddToDeathListKey))
                AddToDeathList?.Invoke();

            if (Input.GetKeyDown(AddToLifetimeListKey))
                AddToLifetimeList?.Invoke();

            if (Input.GetKeyDown(AddToCountLimitListKey))
                AddToCountLimitList?.Invoke();

            if (Input.GetKeyDown(DamageTakenKey))
                DamageTaken?.Invoke();

            if (Input.GetKeyDown(RayCastedKey))
                RayCasted?.Invoke();

            if (Input.GetKeyDown(SpawnKey))
                Spawned?.Invoke();

            MousePosition = Input.mousePosition;
        }
    }
}
