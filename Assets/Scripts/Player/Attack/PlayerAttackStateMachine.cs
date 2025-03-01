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
        [SerializeField, Min(0)] private int damage;
        [SerializeField] private Collider fistLeftCollider;
        [SerializeField] private Collider fistRightCollider;
        [SerializeField] private float attackRange = 1f;
        [SerializeField] private float attackSpeed = 1f;
        [SerializeField] private float throwRadius;
        [SerializeField] private float throwSpeed;

        private PlayerInput _playerInput;
        public Animator Animator { get; private set; }
        public int Damage => damage;
        public float AttackRange => attackRange;
        public Collider FistLeftCollider => fistLeftCollider;
        public Collider FistRightCollider => fistRightCollider;
        private InputAction _attackAction;

        public int AttackCombo { set; get; }

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _attackAction = _playerInput.actions[ControlEnum.Attack1];
            Animator = GetComponent<Animator>();
            _attackAction.performed += OnAttack;
        }
        
        private IEnumerator WaitForAttackAnimation()
        {
            yield return new WaitForSeconds(attackSpeed); 
            SwitchState(new PlayerIdleAttackState(this));
        }
        

        private void OnAttack(InputAction.CallbackContext context)
        {
            SwitchState(new PlayerAttack(this, fistLeftCollider));
            StartCoroutine(WaitForAttackAnimation());
        }

        private void OnDestroy()
        {
            _attackAction.performed -= OnAttack;
        }
    }
}