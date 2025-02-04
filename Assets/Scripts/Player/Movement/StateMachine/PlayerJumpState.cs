using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class PlayerJumpState : PlayerBaseState
    {
        private readonly int _inJumpingHash = Animator.StringToHash("isJumping");
        private readonly int _jumpBlendTreeHash = Animator.StringToHash("JumpBlendTree");
        private const float CrossFadeDuration = 0.1f;
        private const float TimeToJump = 1f;
        private bool _isGrounded;
        private static float _jumpForce = 3f;

        public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.Velocity.y = Physics.gravity.y;
            FaceMoveDirection();
            StateMachine.Animator.SetBool(_inJumpingHash, true);
            StateMachine.Animator.speed = 1.3f;
            JumpCalculation();
            _isGrounded = false;
        }

        public override void Tick()
        {
            StateMachine.Animator.SetFloat(_jumpBlendTreeHash,
                StateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 0.9f : 0f, 0.9f,
                Time.deltaTime);
            
            ApplyGravity();
            if (_isGrounded)
            {
                StateMachine.Animator.SetBool(_inJumpingHash, false);
                StateMachine.SwitchState(new PlayerMoveState(StateMachine));
                return;
            }
            PhysicsUpdate();
        }


        private void PhysicsUpdate()
        {
            CalculateMoveDirection();
            FaceMoveDirection();
            _isGrounded = CheckCollisionOverlap(StateMachine.transform.position);
        }
        
        private void ApplyGravity()
        {
            if (_isGrounded) return;
            StateMachine.Velocity.y += Physics.gravity.y * Time.deltaTime;
            StateMachine.Controller.Move(StateMachine.Velocity * Time.deltaTime); 
        }

        private void JumpCalculation()
        {
            StateMachine.Velocity.y = _jumpForce;
        }

        public override void Exit()
        {
            StateMachine.Velocity.y = Physics.gravity.y;
        }
    }
}