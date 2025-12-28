using System;
using UnityEngine;

namespace Develop._2.Timer
{
    public class DebugProgressView : IProgressUpdater
    {
        public void UpdateProgress(float progress, float limit)
        {
            string currentTime =TimeSpan.FromSeconds(progress).ToString(@"mm\:ss");
            Debug.Log($"Current Time - {currentTime}");
        }

        public void ResetProgress(float limit)
        {
            Debug.Log($"Timer has been reset. Current Time - {limit}");
        }
    }
}
