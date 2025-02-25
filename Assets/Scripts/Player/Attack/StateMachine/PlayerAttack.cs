using Common.Enums;
using Interfaces;
using Player.Movement.StateMachine;
using UnityEngine;

namespace Player.Attack.StateMachine
{
    public class PlayerAttack : PlayerBaseAttackState
    {
        private readonly int _attackHash = Animator.StringToHash(PlayerAnimatorEnum.IsAttack);
        private float _timer = 1f;
        private readonly Vector3 _attackDirection;

        public PlayerAttack(PlayerAttackStateMachine state) : base(state)
        {
            _attackDirection = StateMachine.FistTransform.forward;
        }

        public override void Enter()
        {
            StateMachine.Animator.SetBool(_attackHash, true);
            Damage();
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

        private void Damage()
        {
            if (!Physics.Raycast(StateMachine.FistTransform.position, _attackDirection, out var hit,
                StateMachine.AttackRange)) return;
           
            if (!hit.collider.TryGetComponent(out IDamageable damageable)) return;
           
            damageable.OnTakeDamage(StateMachine.Damage);
          //  Debug.Log("Попал по врагу и нанёс урон!");
        }
    }
}