using Common.StateMachine;

namespace Player.Attack.StateMachine
{
    public abstract class PlayerBaseAttackState: State
    {
        protected readonly PlayerAttackStateMachine StateMachine;

        protected PlayerBaseAttackState(PlayerAttackStateMachine state)
        {
            StateMachine = state;
        }
    }
} 