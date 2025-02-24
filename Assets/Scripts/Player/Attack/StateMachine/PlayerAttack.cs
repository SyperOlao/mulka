using Common.Enums;
using Player.Movement.StateMachine;
using UnityEngine;

namespace Player.Attack.StateMachine
{
    public class PlayerAttack : PlayerBaseAttackState
    {
        private readonly int _attackHash = Animator.StringToHash(PlayerAnimatorEnum.IsAttack);
        private float _timer = 1f;

        public PlayerAttack(PlayerAttackStateMachine state) : base(state)
        {
        }

        public override void Enter()
        {
            StateMachine.Animator.SetBool(_attackHash, true);
        }

        public override void Tick()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                StateMachine.SwitchState(new PlayerIdleAttackState(StateMachine));
            }
        }

        public override void Exit()
        {
            StateMachine.Animator.SetBool(_attackHash, false);
        }
    }
}