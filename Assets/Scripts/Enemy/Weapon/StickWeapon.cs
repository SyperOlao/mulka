using Common.Enums;
using Player.Attack.StateMachine;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Enemy.Weapon
{
    public class StickWeapon : Interfaces.Weapon
    {
        private readonly int _attackHash = Animator.StringToHash(PlayerAnimatorEnum.IsAttack);

        public override void OnAttack(InputAction.CallbackContext context)
        {
            Animator.SetInteger(_attackHash, 5);
            if (StateMachine != null)
            {
                StateMachine.SwitchState(new PlayerMeleeAttack(StateMachine, WeaponCollider));
            }
        }

        public override void EndAttack()
        {
            
            StateMachine.SwitchState(new PlayerIdleAttackState(StateMachine));
            Animator.SetInteger(_attackHash, 4);
        }
    }
}