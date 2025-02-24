using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement.StateMachine
{
    [RequireComponent(typeof(InputReader))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMoveStateMachine: Common.StateMachine.StateMachine
    {
        public Vector3 Velocity;
        public float LookRotationDampFactor { get; private set; } = 10f;

        [SerializeField] public Transform MainCamera;
        
        
        public float MovementSpeed { get; private set; } = 5f;
        public InputReader InputReader { get; private set; }
        public Animator Animator { get; private set; }
        public CharacterController Controller { get; private set; }
        
        private PlayerInput _playerInput;
        public InputAction RunAction { get; private set; }
        public InputAction JumpAction { get; private set; }
      
     

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            RunAction = _playerInput.actions["RunQuick"];
            JumpAction = _playerInput.actions["Jump"];
       
        }
    
        private void Start()
        {
            InputReader = GetComponent<InputReader>();
            Animator = GetComponent<Animator>();
            Controller = GetComponent<CharacterController>();
            
            SwitchState(new PlayerMoveState(this));
        }

        public void SetIdleState()
        {
            SwitchState(new PlayerMoveState(this));
        }

        public void SetMoveState()
        {
            SwitchState(new PlayerIdleState(this));
        }
        
    }
}