using Common.Enums;
using Interfaces;
using Player.Attack.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Enemy.Weapon
{
    public class FistWeapon: Interfaces.Weapon
    {
        [SerializeField] private Collider rightFist;
        
        private readonly int _attackHash = Animator.StringToHash(PlayerAnimatorEnum.AttackFist);
        private int _attackCombo;
        
        public override void OnAttack(InputAction.CallbackContext context)
        {
            
            if (StateMachine == null) return;
            _attackCombo++;
            if (_attackCombo <= 2)
            {
                Animator.SetInteger(_attackHash, 2);
                StateMachine.SwitchState(new PlayerMeleeAttack(StateMachine, WeaponCollider));
            }
            else
            {
                Animator.SetInteger(_attackHash, 3);
                StateMachine.SwitchState(new PlayerMeleeAttack(StateMachine, rightFist));
                _attackCombo = 0;
            }

            
        }
        
        public override void SwitchAnimationToStart()
        {
            Animator.SetInteger(_attackHash, 1);
        }

        public override void SwitchAnimationToEnd()
        {
            Animator.SetInteger(_attackHash, 0);
        }
        
        
        public override void EndAttack()
        {
            StateMachine.SwitchState(new PlayerIdleAttackState(StateMachine));
            Animator.SetInteger(_attackHash, 1);
        }
        
    }
}