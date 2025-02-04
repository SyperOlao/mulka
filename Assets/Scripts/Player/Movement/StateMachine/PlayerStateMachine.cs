using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement.StateMachine
{
    [RequireComponent(typeof(InputReader))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerStateMachine: Common.StateMachine.StateMachine
    {
        public Vector3 Velocity;
        public float LookRotationDampFactor { get; private set; } = 10f;

        [SerializeField] public Transform MainCamera;

        [SerializeField] public LayerMask whatIsGround;
        
        public float MovementSpeed { get; private set; } = 5f;
        public InputReader InputReader { get; private set; }
        public Animator Animator { get; private set; }
        public CharacterController Controller { get; private set; }
        
        private PlayerInput playerInput;
        public InputAction RunAction { get; private set; }
        public InputAction JumpAction { get; private set; }
      
        public Rigidbody Rigidbody { get; private set; }

        public float CollisionOverlapRadius { get; private set; } = 0.1f;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            RunAction = playerInput.actions["RunQuick"];
            JumpAction = playerInput.actions["Jump"];
            Rigidbody = GetComponent<Rigidbody>();
        }
        public void ApplyImpulse(Vector3 force)
        {
            Rigidbody.AddForce(force, ForceMode.Impulse);
        }
        private void Start()
        {
            InputReader = GetComponent<InputReader>();
            Animator = GetComponent<Animator>();
            Controller = GetComponent<CharacterController>();
            
            SwitchState(new PlayerMoveState(this));
        }
        
    }
}