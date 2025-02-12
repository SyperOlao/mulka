using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class PlayerJumpState : PlayerBaseState
    {
        private readonly int _inJumpingHash = Animator.StringToHash("isJumping");
        private readonly int _jumpBlendTreeHash = Animator.StringToHash("JumpBlendTree");
        private bool _isGrounded;
        private const float JumpForce = 3f;
        private readonly float _additionJumpForce;

        public PlayerJumpState(PlayerStateMachine stateMachine, float additionJumpForce = 1f) : base(stateMachine)
        {
            _additionJumpForce = additionJumpForce;
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
            var forceValue = _additionJumpForce * StateMachine.Velocity;
            StateMachine.Controller.Move(forceValue * Time.deltaTime);
        }

        private void JumpCalculation()
        {
            StateMachine.Velocity.y = JumpForce;
        }

        public override void Exit()
        {
            StateMachine.Velocity.y = Physics.gravity.y;
        }
    }
}