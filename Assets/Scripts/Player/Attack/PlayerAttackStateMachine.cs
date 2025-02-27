using System;
using System.Collections.Generic;
using Common.Enums;
using Interfaces;
using Player.Attack.StateMachine;
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
        [SerializeField] private Transform fistRight;
        [SerializeField] private float attackRange = 1f;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float throwRadius;
        [SerializeField] private float throwSpeed;

        private PlayerInput _playerInput;
        public Animator Animator { get; private set; }
        public int Damage => damage;
        public float AttackRange => attackRange;
        public Collider FistLeftCollider => fistLeftCollider;
        public Transform FistRight => fistRight;
        private InputAction _attackAction;
        private readonly HashSet<IDamageable> _enemiesInRange = new();

        public int AttackCombo { set; get; }
        
        
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _attackAction = _playerInput.actions[ControlEnum.Attack1];
            _attackAction.performed += OnAttack;
            Animator = GetComponent<Animator>();
        }


        private void OnAttack(InputAction.CallbackContext context)
        {
            SwitchState(new PlayerAttack(this));
        }

        private void OnDestroy()
        {
            _attackAction.performed -= OnAttack;
        }
    }
}