using System;
using System.Diagnostics;
using Common.Enums;
using JetBrains.Annotations;
using Player.Attack;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interfaces
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private string weaponName;
        [SerializeField] private Collider weaponCollider;
        [SerializeField] private int damage;
        [SerializeField] private Sprite icon;
        [SerializeField] private float attackSpeed;
        [SerializeField] private Animator animator;
        [SerializeField] [CanBeNull] protected GameObject weaponObject = null;
        protected PlayerAttackStateMachine StateMachine;
        private AnimatorStateInfo _stateInfo;

        public string WeaponName { get; set; }
        protected Animator Animator => animator;
        protected Collider WeaponCollider => weaponCollider;

        public int Damage => damage;

        public Sprite Icon => icon;

        protected float AttackSpeed => attackSpeed;

        public float GetCurrentAnimationSpeed()
        {
            _stateInfo = Animator.GetCurrentAnimatorStateInfo(1);
            var clipLength = _stateInfo.length;
            var animatorSpeed = Animator.speed;
            var stateSpeed = _stateInfo.speed;
            var speedMultiplier = Animator.GetFloat(PlayerAnimatorEnum.Speed);
            var realSpeed = animatorSpeed * stateSpeed * speedMultiplier;
            var actualClipLength = clipLength / realSpeed;

            return actualClipLength;
        }

        public void Initialize(PlayerAttackStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }


        public void EquipWeapon()
        {
            if (weaponObject == null) return;
            weaponObject.SetActive(true);
        }

        public void UnEquipWeapon()
        {
            if (weaponObject == null) return;
            weaponObject.SetActive(false);
        }


        public virtual void SwitchAnimationToEnd()
        {
        }

        public virtual void SwitchAnimationToStart()
        {
        }


        public abstract void OnAttack(InputAction.CallbackContext context);
        public abstract void EndAttack();
    }
}