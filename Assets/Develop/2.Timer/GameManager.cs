using UnityEngine;

namespace Develop._2.Timer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _timerLimit;
        [SerializeField] private TimerView _timerViewPrefab;
        [SerializeField] private RectTransform _timerCanvasTransform;
        [SerializeField] private ProgressViewType _progressViewType;

        [SerializeField] private ImagesProgressView _imagesProgressViewPrefab;
        [SerializeField] private SliderProgressView _sliderProgressViewPrefab;

        private IProgressUpdater _progressUpdater;

        private Timer _timer;
        private PlayerInput _playerInput;
        private TimerView _timerView;

        private void Start()
        {
            _timer = new Timer(this);
            _playerInput = new PlayerInput();

            _playerInput.StartTimer += OnStartTimerKeyDown;
            _playerInput.StopTimer += OnStopTimerKeyDown;
            _playerInput.ResetTimer += OnResetTimerKeyDown;

            if (_timerViewPrefab != null)
            {
                _timerView = Instantiate(_timerViewPrefab, _timerCanvasTransform);
                _timerView.Initialize(_timer, _timerLimit);

                _progressUpdater = CreateProgressView();
            }
            else
            {
                _progressUpdater = new DebugProgressView();
            }

            _timer.Ticked += _progressUpdater.UpdateProgress;
        }

        private void Update()
        {
            _playerInput.UpdateInput();
        }

        private void OnDestroy()
        {
            _playerInput.StartTimer -= OnStartTimerKeyDown;
            _playerInput.StopTimer -= OnStopTimerKeyDown;
            _playerInput.ResetTimer -= OnResetTimerKeyDown;

            _timer.Ticked -= _progressUpdater.UpdateProgress;
        }

        private IProgressUpdater CreateProgressView()
        {
            IProgressUpdater progressUpdater = null;

            if(_progressViewType == ProgressViewType.Image)
                progressUpdater = Instantiate(_imagesProgressViewPrefab, _timerView.transform);

            if(_progressViewType == ProgressViewType.Slider)
                progressUpdater = Instantiate(_sliderProgressViewPrefab, _timerView.transform);

            return progressUpdater;
        }

        private void OnStartTimerKeyDown() => _timer.StartProcess(_timerLimit);
        private void OnStopTimerKeyDown() => _timer.StopProcess();
        private void OnResetTimerKeyDown() => _timer.Reset();
    }
}
