using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Develop._2.Timer
{
    public class ImagesProgressView : MonoBehaviour, IProgressUpdater
    {
        [SerializeField] private List<Image> _images;

        private int _maxSize;
        private int _iterator;
        private float _lastProgress;

        private void Awake()
        {
            _maxSize = _images.Count;
            Reset();
        }

        public void UpdateProgress(float progress, float limit)
        {
            if(progress == 0)
                Reset();

            float step = limit / _maxSize;
            float currentProgress = progress - _lastProgress;

            if(currentProgress / step >= 1)
            {
                ShowNext();
                _lastProgress = progress;
            }
        }

        private void ShowNext()
        {
            if (_iterator >= _maxSize)
                return;

            _images[_iterator].gameObject.SetActive(true);
            _iterator++;
        }

        private void Reset()
        {
            _iterator = 0;
            _lastProgress = 0;

            foreach (Image image in _images)
                image.gameObject.SetActive(false);
        }
    }
}
