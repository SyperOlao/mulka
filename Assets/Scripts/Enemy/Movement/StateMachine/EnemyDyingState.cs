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
            StateMachine.Velocity.y = Physics.gravity.y;
            StateMachine.Animator.SetTrigger(_dyingHash);
            StateMachine.NavMeshAgent.enabled = false;
            StateMachine.transform.rotation *= Quaternion.Euler(90, 0, 0);
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}