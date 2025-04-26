using System;
using Interfaces;
using UI.Battle;
using UnityEngine;

namespace Enemy.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField, Min(1)] private int maxHealth = 100;
        public event Action TakeDamage;
        public int CurrentHealth { get; private set; }
        public int MaxHealth => maxHealth;
        private void Start()
        {
            CurrentHealth = maxHealth;
        }

        public void OnTakeDamage(int damage)
        {
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                return;
            }
            CurrentHealth -= damage;
            TakeDamage?.Invoke();
        }
    }
}