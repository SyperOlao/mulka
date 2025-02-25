using System;
using Common.Enums;
using Enemy.FOV;
using Enemy.Health.FaceCamera;
using Enemy.Movement.StateMachine;
using Interfaces;
using UI.Battle;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(FieldOfView))]
    [RequireComponent(typeof(EnemyStateMachine))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Health.Health))]
    public class Enemy: MonoBehaviour, IDamageable
    {
     
        private Health.Health _health;

        private Animator _animator;
        private readonly int _dyingHash = Animator.StringToHash(EnemyAnimatorEnum.Dying);
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health.Health>();
        }

        public void OnTakeDamage(int damage)
        {
            _health.OnTakeDamage(damage);
            if (_health.CurrentHealth <= 0)
            {
                _animator.SetTrigger(_dyingHash);
            }
        }
        
    }
}