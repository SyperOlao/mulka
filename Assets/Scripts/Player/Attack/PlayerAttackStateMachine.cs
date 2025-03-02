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
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _attackAction = _playerInput.actions[ControlEnum.Attack1];
            _switchWeapon = _playerInput.actions[ControlEnum.DrawWeapon];
            _attackAction.performed += OnAttack;
            _switchWeapon.performed += OnSwitch;
            _weapon = availableWeapons[1];
            InitializeWeaponState();

        }

        private void OnSwitch(InputAction.CallbackContext context)
        {
            _currentWeaponIndex++;
            _weapon = availableWeapons[_currentWeaponIndex % availableWeapons.Length];
            InitializeWeaponState();

        }

        private IEnumerator WaitForAttackAnimation()
        {
            yield return new WaitForSeconds(_weapon.AttackSpeed);
            _weapon.EndAttack();
       
        }
        

        private void OnAttack(InputAction.CallbackContext context)
        {
            _weapon.OnAttack(context);
            StartCoroutine(WaitForAttackAnimation());
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
            _attackAction.performed -= OnAttack;
            _switchWeapon.performed -= OnSwitch;
        }
    }
}