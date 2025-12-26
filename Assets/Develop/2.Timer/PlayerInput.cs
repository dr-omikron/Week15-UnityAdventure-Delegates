using System;
using UnityEngine;

namespace Develop._2.Timer
{
    public class PlayerInput
    {
        public event Action StartTimer;
        public event Action StopTimer;
        public event Action ResetTimer;

        private const KeyCode StartTimerKey = KeyCode.S;
        private const KeyCode StopTimerKey = KeyCode.F;
        private const KeyCode ResetTimerKey = KeyCode.R;

        public void UpdateInput()
        {
            if (Input.GetKeyDown(StartTimerKey))
                StartTimer?.Invoke();

            if (Input.GetKeyDown(StopTimerKey))
                StopTimer?.Invoke();

            if (Input.GetKeyDown(ResetTimerKey))
                ResetTimer?.Invoke();
        }
    }
}
