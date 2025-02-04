using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class PlayerRunState : PlayerMoveState
    {
        private const int Acceleration = 2;
        
        public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Enter()
        {
            StateMachine.Velocity.y = Physics.gravity.y;
            
        }

        
        public override void Tick()
        {
            CalculateMoveDirection();
            FaceMoveDirection();
            MovementSpeedCalculation(1f, Acceleration);
            if (StateMachine.JumpAction.IsPressed())
            {
                StateMachine.SwitchState(new PlayerJumpState(StateMachine, 2f));
            }
            if (!StateMachine.RunAction.IsPressed())
            {
                StateMachine.SwitchState(new PlayerMoveState(StateMachine));
            }

        }
        
        public override void Exit()
        {
        }
        
        
        
    }
}