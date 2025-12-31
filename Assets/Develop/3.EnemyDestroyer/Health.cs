using System;
using UnityEngine;

namespace Develop._3.EnemyDestroyer
{
    public class Health
    {
        public event Action<float, float> HealthChanged;

        private readonly int _maxHealth;
        private int _health;

        public Health(int maxHeath)
        {
            _maxHealth = maxHeath;
            _health = maxHeath;
        }

        public void TakeDamage(int damage)
        {
            if(damage <= 0)
                Debug.Log("Damage can't be less or equal to zero");

            _health -= damage;

            if (_health <= 0)
                _health = 0;

            HealthChanged?.Invoke(_health, _maxHealth);
        }

        public bool IsDead() => _health <= 0;
    }
}
