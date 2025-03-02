using System;
using Common.Enums;
using Player.Attack.StateMachine;
using UnityEngine.InputSystem;
using UnityEngine;
using Object = System.Object;

namespace Enemy.Weapon
{
    public class StickWeapon : Interfaces.Weapon
    {
        private readonly int _attackHash = Animator.StringToHash(PlayerAnimatorEnum.IsAttack);

        private readonly Vector3 _localPosition = new(0.114454597f, 0.34719187f, -0.0732419863f);
        private readonly Vector3 _localRotation = new(1.33f, 183f, 94f);

        public override void OnAttack(InputAction.CallbackContext context)
        {
            Animator.SetInteger(_attackHash, 5);
            if (StateMachine != null)
            {
                StateMachine.SwitchState(new PlayerMeleeAttack(StateMachine, WeaponCollider));
            }
        }

        public override void EquipWeapon()
        {
            EquipWeapon(weaponObject, _localPosition, _localRotation);
        }

        public override void EndAttack()
        {
            StateMachine.SwitchState(new PlayerIdleAttackState(StateMachine));
            Animator.SetInteger(_attackHash, 4);
        }
    }
}