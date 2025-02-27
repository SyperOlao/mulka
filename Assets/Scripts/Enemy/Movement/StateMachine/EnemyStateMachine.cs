using System;
using System.Collections.Generic;
using DataClasses;
using Enemy.FOV;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Movement.StateMachine
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyStateMachine : Common.StateMachine.StateMachine
    {
        public Vector3 Velocity;

        public float PointRadius => 0.1f;

        public float MovementSpeed { get; private set; } = 5f;
        public Animator Animator { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        [SerializeField] private FieldOfView fieldOfView;
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private int radiusStartFiled;
        public Vector3 StartPosition => startPosition;
        public int RadiusStartFiled => radiusStartFiled; 
        public FieldOfView FieldOfView => fieldOfView;
        public NavMeshAgent NavMeshAgent { get; private set; }
        
        public CapsuleCollider CapsuleCollider { get; private set; }
        

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            CapsuleCollider = GetComponent<CapsuleCollider>();
        }

        private void Start()
        {
            SwitchState(new EnemyMoveState(this));
        }
    }
}