using UnityEngine;
using UnityEngine.UI;

namespace Develop._2.Timer
{
    public class SliderProgressView : MonoBehaviour, IProgressUpdater
    {
        [SerializeField] private Image _slider;

        private void Awake()
        {
            ResetView();
        }

        public void UpdateProgress(float progress, float limit)
        {
            SetPercent(progress / limit);
        }

        public void ResetProgress(float limit) => ResetView();

        private void SetPercent(float percent)
        {
            if(percent < 0)
            {
                Debug.Log("Percent is less than 0");
                return;
            }

            _slider.fillAmount = percent;
        }

        private void ResetView() => _slider.fillAmount = 1;
    }
}
