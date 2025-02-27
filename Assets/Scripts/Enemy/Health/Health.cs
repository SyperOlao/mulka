using System;
using Interfaces;
using UI.Battle;
using UnityEngine;

namespace Enemy.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField, Min(1)] private int maxHealth = 100;
        public float CurrentHealth { get; private set; }

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
        }
    }
}