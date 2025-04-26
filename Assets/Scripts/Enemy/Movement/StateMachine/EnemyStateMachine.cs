using System;
using System.Collections.Generic;
using DataClasses;
using Enemy.FOV;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Movement.StateMachine
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyStateMachine : Common.StateMachine.StateMachine
    {
        public Vector3 Velocity;
        
        [SerializeField] private FieldOfView fieldOfView;
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private int radiusStartFiled;
        [SerializeField] private EnemyWeapon weapon;

        public float PointRadius => 0.1f;

        public float MovementSpeed { get; private set; } = 5f;
        public Animator Animator { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Vector3 StartPosition => startPosition;
        public int RadiusStartFiled => radiusStartFiled; 
        public FieldOfView FieldOfView => fieldOfView;
        public NavMeshAgent NavMeshAgent { get; private set; }
        [SerializeField] private Transform playerTransform;
        public Transform PlayerTransform => playerTransform;
        public EnemyWeapon Weapon => weapon;
        
        public CapsuleCollider CapsuleCollider { get; private set; }
        private Quaternion _forwardOffset;
        private Quaternion _initialRotation;
        public Quaternion ForwardOffset => _forwardOffset;
        public Quaternion InitialRotation => _initialRotation;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            CapsuleCollider = GetComponent<CapsuleCollider>();
            
            // _initialRotation = transform.rotation;
            // Vector3 initialForward = transform.forward;
            // initialForward.y = 0;
            //
            // if (initialForward.sqrMagnitude < 0.0001f)
            //     initialForward = Vector3.forward;
            //
            // initialForward.Normalize();
            //
            // _forwardOffset = Quaternion.FromToRotation(initialForward, Vector3.forward);
        }

        private void Start()
        {
            SwitchState(new EnemyMoveState(this));
        }

        public void Death()
        {
            SwitchState(new EnemyDyingState(this));
        }
    }
}