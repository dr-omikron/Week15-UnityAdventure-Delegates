using UnityEngine;
using UnityEngine.UI;

namespace Develop._2.Timer
{
    public class SliderProgressView : MonoBehaviour, IProgressUpdater
    {
        [SerializeField] private Image _slider;

        private void Awake()
        {
            Reset();
        }

        public void UpdateProgress(float progress, float limit)
        {
            SetPercent(progress / limit);
        }

        private void SetPercent(float percent)
        {
            if(percent < 0)
            {
                Debug.Log("Percent is less than 0");
                return;
            }

            _slider.fillAmount = percent;
        }

        private void Reset() => _slider.fillAmount = 0;
    }
}
