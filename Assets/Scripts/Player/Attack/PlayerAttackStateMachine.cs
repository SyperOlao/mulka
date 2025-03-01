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
        [SerializeField] private Weapon weapon;
        private PlayerInput _playerInput;
        private InputAction _attackAction;
     
        private int _attackCombo;
        public Weapon Weapon => weapon;
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _attackAction = _playerInput.actions[ControlEnum.Attack1];
            _attackAction.performed += OnAttack;
            if (weapon != null)
            {
                weapon.Initialize(this);
            }
           
        }
        
        private IEnumerator WaitForAttackAnimation()
        {
            yield return new WaitForSeconds(weapon.AttackSpeed);
            weapon.EndAttack();
       
        }
        

        private void OnAttack(InputAction.CallbackContext context)
        {
            weapon.OnAttack(context);
            StartCoroutine(WaitForAttackAnimation());
        }

        private void OnDestroy()
        {
            _attackAction.performed -= OnAttack;
        }
    }
}