using Common.Enums;
using Player.Movement.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    [RequireComponent(typeof(PlayerCondition))]
    [RequireComponent(typeof(InputReader))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMoveStateMachine : Common.StateMachine.StateMachine
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
        public PlayerCondition PlayerCondition { get; private set; }


        private void Awake()
        {
            PlayerCondition = GetComponent<PlayerCondition>();
            _playerInput = GetComponent<PlayerInput>();
            InputReader = GetComponent<InputReader>();
            Animator = GetComponent<Animator>();
            Controller = GetComponent<CharacterController>();
            RunAction = _playerInput.actions[ControlEnum.RunQuick];
            JumpAction = _playerInput.actions[ControlEnum.Jump];
        }

        public void StopState()
        {
            SwitchState(new PlayerIdleState(this));
        }
        
        public void Death()
        {
            SwitchState(new PlayerDeathState(this));
        }

        public void MoveState()
        {
            SwitchState(new PlayerMoveState(this));
        }


        private void Start()
        {
            SwitchState(new PlayerMoveState(this));
        }
    }
}