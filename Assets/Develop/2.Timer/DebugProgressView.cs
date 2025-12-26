using System;
using UnityEngine;

namespace Develop._2.Timer
{
    public class DebugProgressView : IProgressUpdater
    {
        public void UpdateProgress(float progress, float limit)
        {
            string elapsedTime =TimeSpan.FromSeconds(progress).ToString(@"mm\:ss");
            Debug.Log($"Elapsed Time - {elapsedTime}");
        }
    }
}
