using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class PlayerDeathState : PlayerBaseState
    {
        public PlayerDeathState(PlayerMoveStateMachine moveStateMachine) : base(moveStateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log("PlayerDeathState");
            MoveStateMachine.RunAction.Disable();
            MoveStateMachine.JumpAction.Disable();
        }

        public override void Tick()
        {
          
        }

        public override void Exit()
        {
            
        }
    }
}