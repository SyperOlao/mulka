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

        public float PointRadius { get; set; } = 0.01f;

        public float LookRotationDampFactor { get; private set; } = 10f;

        public float MovementSpeed { get; private set; } = 5f;
        public Animator Animator { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        [SerializeField] private FieldOfView _fieldOfView;
        [SerializeField] private List<PointsWithTimeStandby> _points;
        [SerializeField] private int _currentPoint;
        
        public List<PointsWithTimeStandby> Points => _points;
        public FieldOfView FieldOfView => _fieldOfView;
        public int CurrentPoint
        {
            get => _currentPoint;
            set => _currentPoint = value;
        }

        private void OnEnable()
        {
            FieldOfView.IsPlayerVisible += SwitchToWarning;
        }

        private void OnDisable()
        {
            FieldOfView.IsPlayerVisible -= SwitchToWarning;
        }


        private void SwitchToWarning()
        {
            if (FieldOfView.CanSeePlayer && CurrentState is not (EnemyAttackState or EnemyWarningState))
            {
                SwitchState(new EnemyWarningState(this));        
            }
        }

        private void Start()
        {
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();

            SwitchState(new EnemyMoveState(this));
        }
    }
}