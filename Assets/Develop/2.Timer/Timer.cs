using System;
using System.Collections;
using UnityEngine;

namespace Develop._2.Timer
{
    public class Timer
    {
        public event Action<float, float> Ticked;
        public event Action<float> Reset;

        private float _timeLimit;
        private float _currentTime;

        private readonly MonoBehaviour _coroutineRunner;
        private Coroutine _process;

        public Timer(MonoBehaviour coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void SetTime(float time)
        {
            if(time <= 0)
                Debug.LogError("Time must be greater than zero");

            _timeLimit = time;
            _currentTime = _timeLimit;
        }

        public void StartProcess()
        {
            StopProcess();
            _process = _coroutineRunner.StartCoroutine(Process());
        }

        public void StopProcess()
        {
            if(_process != null)
            {
                _coroutineRunner.StopCoroutine(_process);
                _process =  null;
            }
        }

        public void ResetTime()
        {
            if(InProcess() == false)
            {
                _currentTime = _timeLimit;
                Reset?.Invoke(_timeLimit);
            }
        }

        public bool InProcess() => _process != null;

        private IEnumerator Process()
        {
            while (_currentTime > 0)
            {
                _currentTime -= Time.deltaTime;

                if(_currentTime > _timeLimit)
                    _currentTime = _timeLimit;

                Ticked?.Invoke(_currentTime, _timeLimit);

                yield return null;
            }

            _process =  null;
        }
    }
}
