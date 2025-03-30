using Common.Enums;
using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyDyingState : EnemyBaseState
    {
        private readonly int _dyingHash = Animator.StringToHash(EnemyAnimatorEnum.Dying);

        public EnemyDyingState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.CapsuleCollider.enabled = false;
            StateMachine.Velocity.y = Physics.gravity.y;
            StateMachine.Animator.SetTrigger(_dyingHash);
            StateMachine.NavMeshAgent.enabled = false;
        }
        

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}