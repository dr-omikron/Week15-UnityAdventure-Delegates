using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Develop._2.Timer
{
    public class ImagesProgressView : MonoBehaviour, IProgressUpdater
    {
        private const float StepThreshold = 0.99f;
        
        [SerializeField] private List<Image> _images;

        private int _maxSize;
        private int _iterator;
        private float _lastProgress;

        public void Initialize(float timeLimit)
        {
            _maxSize = _images.Count;
            ResetView(timeLimit);
        }

        public void UpdateProgress(float progress, float limit)
        {
            float step = limit / _maxSize;
            float currentProgress = _lastProgress - progress;

            if(currentProgress / step >= StepThreshold)
            {
                NextStep();
                _lastProgress = progress;
            }
        }

        public void ResetProgress(float limit) => ResetView(limit);

        private void NextStep()
        {
            if (_iterator < 0)
                return;

            _images[_iterator].gameObject.SetActive(false);
            _iterator--;
        }

        private void ResetView(float timeLimit)
        {
            _iterator = _maxSize - 1;
            _lastProgress = timeLimit;

            foreach (Image image in _images)
                image.gameObject.SetActive(true);
        }
    }
}
