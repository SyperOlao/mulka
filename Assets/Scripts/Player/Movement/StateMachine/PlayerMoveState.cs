using Unity.VisualScripting;
using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class PlayerMoveState : PlayerBaseState
    {
        private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int _moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;
   


        public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.Velocity.y = Physics.gravity.y;

            StateMachine.Animator.CrossFadeInFixedTime(_moveBlendTreeHash, CrossFadeDuration);
        }

        public override void Tick()
        {
            CalculateMoveDirection();
            FaceMoveDirection();
            MovementSpeedCalculation(0.9f);
            if (StateMachine.JumpAction.IsPressed())
            {
                StateMachine.SwitchState(new PlayerJumpState(StateMachine));
                return;
            }

            if (StateMachine.RunAction.IsPressed())
            {
                StateMachine.SwitchState(new PlayerRunState(StateMachine));
                return;
            }

        }

    

        protected void MovementSpeedCalculation(float animationSpeed, int acceleration = 1)
        {
            StateMachine.Animator.SetFloat(_moveSpeedHash,
                StateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? animationSpeed : 0f, AnimationDampTime,
                Time.deltaTime);
            Move(acceleration);
        }

        public override void Exit()
        {
        }
    }
}