using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class PlayerJumpState : PlayerBaseState
    {
        private readonly int _inJumpingHash = Animator.StringToHash("isJumping");
        private bool _isGrounded;
        private const float JumpForce = 0.5f;
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

            var gravityScale = StateMachine.Velocity.y > 0 ? 0.7f : 1.5f;
            StateMachine.Velocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;

            var forceValue = _additionJumpForce * StateMachine.Velocity;
            StateMachine.Controller.Move(forceValue * Time.deltaTime);
        }

        private void JumpCalculation()
        {
            StateMachine.Velocity.y = Mathf.Sqrt(2 * JumpForce * -Physics.gravity.y);
        }
        public override void Exit()
        {
            StateMachine.Velocity.y = Physics.gravity.y;
        }
    }
}