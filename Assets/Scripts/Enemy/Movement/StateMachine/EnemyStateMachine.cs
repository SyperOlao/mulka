using System;
using System.Collections.Generic;
using DataClasses;
using Enemy.FOV;
using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyStateMachine : Common.StateMachine.StateMachine
    {
        public Vector3 Velocity;

        public float PointRadius { get; set; } = 3f;
        
        public float MovementSpeed { get; private set; } = 5f;
        public Animator Animator { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        [SerializeField] private FieldOfView fieldOfView;
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private int radiusStartFiled;
        
        public Vector3 StartPosition => startPosition;
        public int RadiusStartFiled => radiusStartFiled;
        public FieldOfView FieldOfView => fieldOfView;

      
        // private void OnEnable()
        // {
        //     FieldOfView.IsPlayerVisible += SwitchToWarning;
        // }
        //
        // private void OnDisable()
        // {
        //     FieldOfView.IsPlayerVisible -= SwitchToWarning;
        // }


        private void SwitchToWarning()
        {
            if (FieldOfView.CanSeePlayer && CurrentState is not (EnemyAttackState or EnemyWarningState))
            {
                SwitchState(new EnemyWarningState(this));
            }
        }

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            SwitchState(new EnemyMoveState(this));
        }
    }
}