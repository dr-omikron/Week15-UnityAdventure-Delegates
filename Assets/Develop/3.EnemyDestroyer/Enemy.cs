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


        private Health _health;
        private bool _isSelected;

        public bool IsDead => _health.IsDead();

        public void Initialize(int maxHealth)
        {
            _health = new Health(maxHealth);
        }

        public void TakeDamage(int damage) => _health.TakeDamage(damage);

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
