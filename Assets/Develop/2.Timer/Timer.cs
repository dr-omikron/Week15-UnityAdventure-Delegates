using System;
using System.Collections;
using UnityEngine;

namespace Develop._2.Timer
{
    public class Timer
    {
        public event Action<float, float> Ticked;

        private float _timeLimit;
        private float _elapsedTime;

        private readonly MonoBehaviour _coroutineRunner;
        private Coroutine _process;

        public Timer(MonoBehaviour coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void StartProcess(float time)
        {
            _timeLimit = time;

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

        public void Reset()
        {
            if(InProcess() == false)
            {
                _elapsedTime = 0;
                Ticked?.Invoke(_elapsedTime, _timeLimit);
            }
        }

        private bool InProcess() => _process != null;

        private IEnumerator Process()
        {
            while (_elapsedTime < _timeLimit)
            {
                _elapsedTime += Time.deltaTime;

                if(_elapsedTime > _timeLimit)
                    _elapsedTime = _timeLimit;

                Ticked?.Invoke(_elapsedTime, _timeLimit);

                yield return null;
            }

            _process =  null;
        }
    }
}
