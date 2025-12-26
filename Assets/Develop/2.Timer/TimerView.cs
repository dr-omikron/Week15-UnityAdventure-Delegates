using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Develop._2.Timer
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _pauseButton;

        [SerializeField] private TMP_Text _timerText;

        private Timer _timer;
        private float _timeLimit;

        public void Initialize(Timer timer, float timeLimit)
        {
            _timer = timer;
            _timeLimit = timeLimit;

            _timer.Ticked += OnTimerTicked;

            _startButton.onClick.AddListener(OnStartButtonClicked);
            _resetButton.onClick.AddListener(OnResetButtonClicked);
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }

        private void OnStartButtonClicked() => _timer.StartProcess(_timeLimit);
        private void OnResetButtonClicked() => _timer.Reset();
        private void OnPauseButtonClicked() => _timer.StopProcess();

        private void OnTimerTicked(float elapsedTime, float timeLimit) 
            => _timerText.text = TimeSpan.FromSeconds(elapsedTime).ToString(@"mm\:ss");

        private void OnDestroy()
        {
            _timer.Ticked -= OnTimerTicked;

            _startButton.onClick.RemoveListener(OnStartButtonClicked);
            _resetButton.onClick.RemoveListener(OnResetButtonClicked);
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
        }

    }
}
