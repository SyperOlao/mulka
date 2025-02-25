using System.Collections.Generic;
using Common.Enums;
using Common.Utils;
using Interfaces;
using Player.Movement.StateMachine;
using UnityEngine;

namespace Player.Attack.StateMachine
{
    public class PlayerAttack : PlayerBaseAttackState
    {
        private readonly int _attackHash = Animator.StringToHash(PlayerAnimatorEnum.IsAttack);
        private float _timer = 1f;
        private List<IDamageable> _alreadyDamagedEnemy = new List<IDamageable>();

        public PlayerAttack(PlayerAttackStateMachine state) : base(state)
        {
        }

        public override void Enter()
        {
            StateMachine.Animator.SetInteger(_attackHash, 1);
            Damage();
        }

        public override void Tick()
        {
            _timer -= Time.deltaTime;
            Damage();
            if (_timer <= 0)
            {
                StateMachine.SwitchState(new PlayerIdleAttackState(StateMachine));
            }
        }

        public override void Exit()
        {
            StateMachine.Animator.SetInteger(_attackHash, 4);
        }

        private void Damage()
        {
            var attackDirection = StateMachine.FistLeft.forward;
            var attackStartPos1 = StateMachine.FistLeft.position; 
            var attackStartPos =attackStartPos1 + attackDirection * 0.1f;

            DebugHelper.DebugPath(attackStartPos, attackStartPos1);
            if (!Physics.Raycast(attackStartPos, attackStartPos1, out var hit,
                StateMachine.AttackRange)) return;
           
            if (!hit.collider.TryGetComponent(out IDamageable damageable)) return;
           
            damageable.OnTakeDamage(StateMachine.Damage);
            StateMachine.SwitchState(new PlayerIdleAttackState(StateMachine));
          
          //  Debug.Log("Попал по врагу и нанёс урон!");
        }
    }
}