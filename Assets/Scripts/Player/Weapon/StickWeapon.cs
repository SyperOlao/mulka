using Common.Enums;
using Player.Attack.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Weapon
{
    public class StickWeapon : Interfaces.Weapon
    {
        private readonly int _attackHash = Animator.StringToHash(PlayerAnimatorEnum.AttackStick);
        
       
        public override void OnAttack(InputAction.CallbackContext context)
        {
            Animator.SetFloat(PlayerAnimatorEnum.Speed, AttackSpeed);
            Animator.SetInteger(_attackHash, 2);
            if (StateMachine != null)
            {
                StateMachine.SwitchState(new PlayerMeleeAttack(StateMachine,new [] { WeaponCollider }));
            }
        }

        
        public override void SwitchAnimationToEnd()
        {
            Animator.SetInteger(_attackHash, 0);
        }

        public override void SwitchAnimationToStart()
        {
            Animator.SetInteger(_attackHash, 1);
        }
        

        public override void EndAttack()
        {
            StateMachine.SwitchState(new PlayerIdleAttackState(StateMachine));
            Animator.SetInteger(_attackHash, 1);
        }
    }
}