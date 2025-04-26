namespace Player.Movement.StateMachine
{
    public class PlayerDeathState : PlayerBaseState
    {
        public PlayerDeathState(PlayerMoveStateMachine moveStateMachine) : base(moveStateMachine)
        {
        }

        public override void Enter()
        {
            MoveStateMachine.RunAction.Disable();
        }

        public override void Tick()
        {
          
        }

        public override void Exit()
        {
            
        }
    }
}