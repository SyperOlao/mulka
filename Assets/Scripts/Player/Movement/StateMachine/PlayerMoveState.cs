using Common.Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class PlayerMoveState : PlayerBaseState
    {
        private readonly int _moveSpeedHash = Animator.StringToHash(PlayerAnimatorEnum.MoveSpeed);
        private readonly int _moveBlendTreeHash = Animator.StringToHash(PlayerAnimatorEnum.MoveBlendTree);
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;
   


        public PlayerMoveState(PlayerMoveStateMachine moveStateMachine) : base(moveStateMachine)
        {
        }

        public override void Enter()
        {
            MoveStateMachine.Velocity.y = Physics.gravity.y;

            MoveStateMachine.Animator.CrossFadeInFixedTime(_moveBlendTreeHash, CrossFadeDuration);
        }

        public override void Tick()
        {
            CalculateMoveDirection();
            FaceMoveDirection();
            MovementSpeedCalculation(0.9f);
            if (MoveStateMachine.JumpAction.IsPressed())
            {
                MoveStateMachine.SwitchState(new PlayerJumpState(MoveStateMachine));
                return;
            }

            if (MoveStateMachine.RunAction.IsPressed())
            {
                MoveStateMachine.SwitchState(new PlayerRunState(MoveStateMachine));
                return;
            }

        }

    

        protected void MovementSpeedCalculation(float animationSpeed, int acceleration = 1)
        {
            MoveStateMachine.Animator.SetFloat(_moveSpeedHash,
                MoveStateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? animationSpeed : 0f, AnimationDampTime,
                Time.deltaTime);
            Move(acceleration);
        }

        public override void Exit()
        {
        }
    }
}