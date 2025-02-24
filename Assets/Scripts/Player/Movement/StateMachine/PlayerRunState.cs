using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class PlayerRunState : PlayerMoveState
    {
        private const int Acceleration = 2;
        
        public PlayerRunState(PlayerMoveStateMachine moveStateMachine) : base(moveStateMachine)
        {
        }
        
        public override void Enter()
        {
            MoveStateMachine.Velocity.y = Physics.gravity.y;
            
        }

        
        public override void Tick()
        {
            CalculateMoveDirection();
            FaceMoveDirection();
            MovementSpeedCalculation(1f, Acceleration);
            if (MoveStateMachine.JumpAction.IsPressed())
            {
                MoveStateMachine.SwitchState(new PlayerJumpState(MoveStateMachine, 2f));
            }
            if (!MoveStateMachine.RunAction.IsPressed())
            {
                MoveStateMachine.SwitchState(new PlayerMoveState(MoveStateMachine));
            }

        }
        
        public override void Exit()
        {
        }
        
        
        
    }
}