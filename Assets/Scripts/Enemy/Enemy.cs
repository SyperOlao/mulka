using System;
using Enemy.FOV;
using Enemy.Health.FaceCamera;
using Enemy.Movement.StateMachine;
using Interfaces;
using Spawner;
using UI.Battle;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(FieldOfView))]
    [RequireComponent(typeof(EnemyStateMachine))]
    public class Enemy: MonoBehaviour, IDamageable
    {
        [SerializeField] private Health.Health health;

        public Health.Health Health => health;
        private EnemySpawner _enemySpawner;

        private void Awake()
        {
            _enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        public void OnTakeDamage(float damage)
        {
            health.TakeDamage(damage);

            if (health.CurrentHealth <= 0)
            {
                _enemySpawner.ActionOnRelease(gameObject);
            }
        }
        
    }
}