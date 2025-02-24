using Common.Enums;
using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class PlayerJumpState : PlayerBaseState
    {
        private readonly int _inJumpingHash = Animator.StringToHash(PlayerAnimatorEnum.IsJumping);
        private bool _isGrounded;
        private const float JumpForce = 0.5f;
        private readonly float _additionJumpForce;

        public PlayerJumpState(PlayerMoveStateMachine moveStateMachine, float additionJumpForce = 1f) : base(moveStateMachine)
        {
            _additionJumpForce = additionJumpForce;
        }

        public override void Enter()
        {
            MoveStateMachine.Velocity.y = Physics.gravity.y;
            FaceMoveDirection();
            MoveStateMachine.Animator.SetBool(_inJumpingHash, true);
            JumpCalculation();
            _isGrounded = false;
        }

        public override void Tick()
        {
            ApplyGravity();
            if (_isGrounded)
            {
                MoveStateMachine.Animator.SetBool(_inJumpingHash, false);
                MoveStateMachine.SwitchState(new PlayerMoveState(MoveStateMachine));
                return;
            }

            PhysicsUpdate();
        }


        private void PhysicsUpdate()
        {
            CalculateMoveDirection();
            FaceMoveDirection();
            _isGrounded = CheckCollisionOverlap(MoveStateMachine.transform.position);
        }

        private void ApplyGravity()
        {
            if (_isGrounded) return;

            var gravityScale = MoveStateMachine.Velocity.y > 0 ? 0.7f : 1.5f;
            MoveStateMachine.Velocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;

            var forceValue = _additionJumpForce * MoveStateMachine.Velocity;
            MoveStateMachine.Controller.Move(forceValue * Time.deltaTime);
        }

        private void JumpCalculation()
        {
            MoveStateMachine.Velocity.y = Mathf.Sqrt(2 * JumpForce * -Physics.gravity.y);
        }
        public override void Exit()
        {
            MoveStateMachine.Velocity.y = Physics.gravity.y;
        }
    }
}