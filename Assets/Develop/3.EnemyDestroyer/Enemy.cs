using System;
using System.Collections.Generic;
using UnityEngine;

namespace Develop._3.EnemyDestroyer
{
    public class Enemy : MonoBehaviour, ISelectable, IDestroyable, ISelectNotifier
    {
        public event Action<float, float> HealthChanged
        {
            add => _health.HealthChanged += value;
            remove => _health.HealthChanged -= value;
        }

        public event Action Selected;
        public event Action Deselected;

        public event Action<int, int> CounterChanged;
        public event Action<float, float> LifetimeChanged;
        public event Action DeadConditionActivated;

        private Health _health;
        private bool _isSelected;
        private bool _isLifeTimeConditionActivated;
        private float _lifeTime;
        private float _maxLifeTime;

        public List<Func<bool>> DestroyConditions { get; } =  new List<Func<bool>>();
        public bool IsDead => _health.IsDead();
        public float LifeTime => _lifeTime;
        public int Counter { get; private set; }

        private void Update()
        {
            _lifeTime += Time.deltaTime;

            if(_isLifeTimeConditionActivated)
                IsLifeTimeConditionActivated(_lifeTime, _maxLifeTime);
        }

        public void Initialize(int maxHealth)
        {
            _health = new Health(maxHealth);
        }

        public void IsLifeTimeConditionActivated(float lifeTime, float maxLifeTime)
        {
            _maxLifeTime = maxLifeTime;
            _isLifeTimeConditionActivated = true;
            LifetimeChanged?.Invoke(lifeTime, maxLifeTime);
        }

        public void IsDeadConditionActivated() => DeadConditionActivated?.Invoke();

        public void AddCounter(int counter, int maxCount)
        {
            Counter += counter;
            CounterChanged?.Invoke(Counter, maxCount);
        }

        public void TakeDamage(int damage) => _health.TakeDamage(damage);

        public void AddDestroyCondition(Func<bool> condition)
        {
            if (DestroyConditions.Contains(condition))
                return;

            DestroyConditions.Add(condition);
        }

        public void Select()
        {
            _isSelected = true;
            Selected?.Invoke();
        }

        public void Deselect()
        {
            _isSelected = false;
            Deselected?.Invoke();
        }

        public bool IsSelected() => _isSelected;
        public void Destroy() => Destroy(gameObject);
    }
}
