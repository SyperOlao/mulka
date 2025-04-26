using UnityEngine;
using System;

namespace Enemy.Movement.StateMachine
{
    public class EnemyAttackState : EnemyBaseState
    {
        public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            
            StateMachine.Weapon.AttackAnimationEnded += OnAttackFinished;
            StateMachine.Weapon.OnAttack();
        }
        
        private void OnAttackFinished()
        {
            StateMachine.SwitchState(new EnemyAngryState(StateMachine));
        }

        public override void Tick()
        {
            StateMachine.Weapon.CheckForHits();

        }

        public override void Exit()
        {
            StateMachine.Weapon.AttackAnimationEnded -= OnAttackFinished;
        }
    }
}