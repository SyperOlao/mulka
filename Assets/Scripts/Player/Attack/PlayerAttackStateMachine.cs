using System;
using System.Collections;
using System.Collections.Generic;
using Common.Enums;
using Enemy.Movement.StateMachine;
using Interfaces;
using Player.Attack.StateMachine;
using Player.Movement;
using Player.Movement.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Attack
{
    [RequireComponent(typeof(PlayerCondition))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAttackStateMachine : Common.StateMachine.StateMachine
    {
        [SerializeField] private Weapon[] availableWeapons;
        private Weapon _weapon;
        private PlayerInput _playerInput;
        private InputAction _attackAction;
        private InputAction _switchWeapon;
        public Weapon Weapon => _weapon;
        private int _currentWeaponIndex;
        private Coroutine _attackCoroutine;
        private Coroutine _isAttackStateCoroutine;
        private PlayerCondition _playerCondition;
        private readonly int _isAttackStateTime = 10; 
        
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
       
            _attackAction = _playerInput.actions.FindAction("Attack1");
            _playerCondition = GetComponent<PlayerCondition>();
            _attackAction = _playerInput.actions[ControlEnum.Attack1];
            _switchWeapon = _playerInput.actions[ControlEnum.DrawWeapon];
            _attackAction.started += OnAttack2;
            _switchWeapon.performed += OnSwitch;
            _weapon = availableWeapons[0];
            _weapon.EquipWeapon();
            InitializeWeaponState();
        }
        private void OnSwitch(InputAction.CallbackContext context)
        {
            _currentWeaponIndex++;
            _weapon.UnEquipWeapon();
            _weapon.SwitchAnimationToEnd();
            _weapon = availableWeapons[_currentWeaponIndex % availableWeapons.Length];
            _weapon.EquipWeapon();
            _weapon.SwitchAnimationToStart();
            InitializeWeaponState();
        }

        private IEnumerator WaitForAttackAnimation()
        {
            yield return new WaitForSeconds(_weapon.AttackSpeed);
            _weapon.EndAttack();
        }
        
        private IEnumerator WaitForIsAttackStateTime()
        {
            yield return new WaitForSeconds(_isAttackStateTime);
            _playerCondition.IsAttack = false;
            _weapon.SwitchAnimationToEnd();
        }


        private void OnAttack2(InputAction.CallbackContext context)
        {
            _playerCondition.IsAttack = true;
            StopIsAttackState();
            _isAttackStateCoroutine = StartCoroutine(WaitForIsAttackStateTime());
            _weapon.OnAttack(context);
            _attackCoroutine = StartCoroutine(WaitForAttackAnimation());
        }
        
        private void StopIsAttackState()
        {
            if (_isAttackStateCoroutine == null) return;
            StopCoroutine(_isAttackStateCoroutine);
            _isAttackStateCoroutine = null;
        }
        
        private void StopAttack()
        {
            if (_attackCoroutine == null) return;
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        private void InitializeWeaponState()
        {
            if (_weapon != null)
            {
                _weapon.Initialize(this);
            }
        }

        private void OnDestroy()
        {
            _attackAction.performed -= OnAttack2;
            _switchWeapon.performed -= OnSwitch;
        }
    }
}