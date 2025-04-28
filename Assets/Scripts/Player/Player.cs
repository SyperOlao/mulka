using Common.Enums;
using Enemy.Health;
using Interfaces;
using Player.Movement;
using Player.Movement.StateMachine;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMoveStateMachine))]
    public class Player: MonoBehaviour, IPlayer
    {
        private Health _health;
        private Animator _animator;
        private PlayerMoveStateMachine _playerMoveStateMachine;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
            _playerMoveStateMachine = GetComponent<PlayerMoveStateMachine>();
        }
        
        public void OnTakeDamage(int damage)
        {
            _health.OnTakeDamage(damage);
            if (_health.CurrentHealth <= 0)
            {
                _animator.SetTrigger(PlayerAnimatorEnum.Death);
                _playerMoveStateMachine.Death();
            }
            else
            {
                _animator.SetTrigger(PlayerAnimatorEnum.Hit);
            }
        }

        public void EndDamage()
        {
          
        }
    }
}