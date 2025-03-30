using System;
using Common.Enums;
using Player.Attack.StateMachine;
using UnityEngine.InputSystem;
using UnityEngine;


namespace Enemy.Weapon
{
    public class StickWeapon : Interfaces.Weapon
    {
        private readonly int _attackHash = Animator.StringToHash(PlayerAnimatorEnum.AttackStick);

        private readonly Vector3 _localPosition = new(0.114454597f, 0.34719187f, -0.0732419863f);
        private readonly Vector3 _localRotation = new(1.33f, 183f, 94f);
       
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
        
        public override void EquipWeapon()
        {
            EquipWeapon(weaponObject, _localPosition, _localRotation);
        }

        public override void EndAttack()
        {
            StateMachine.SwitchState(new PlayerIdleAttackState(StateMachine));
            Animator.SetInteger(_attackHash, 1);
        }
    }
}