using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class PlayerMoveState: PlayerBaseState
    {
        private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int _moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;
        private const int Acceleration = 3;
        

        public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            StateMachine.Velocity.y = Physics.gravity.y;

            StateMachine.Animator.CrossFadeInFixedTime(_moveBlendTreeHash, CrossFadeDuration);
            
        }

        public override void Tick()
        {
            CalculateMoveDirection();
            FaceMoveDirection();
        

            if (StateMachine.RunAction.IsPressed())
            {
                StateMachine.Animator.SetFloat(_moveSpeedHash, StateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
                Move(Acceleration);
                return;
            }
            Move();
            StateMachine.Animator.SetFloat(_moveSpeedHash, StateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 0.9f : 0f, AnimationDampTime, Time.deltaTime);
        }

        public override void Exit()
        {
            
        }
        
    }
}