using Common.Utils;
using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyMoveState : EnemyBaseState
    {
        private readonly int _moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float CrossFadeDuration = 0.1f;
        private Vector3 _targetPosition;
        private int _targetWaitTime;

        public EnemyMoveState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.Velocity.y = Physics.gravity.y;

            StateMachine.Animator.CrossFadeInFixedTime(_moveBlendTreeHash, CrossFadeDuration);
            _targetPosition = GetRoamingPosition();
            _targetWaitTime = RandomPositionHelper.GetRandomTime();
        }

        public override void Tick()
        {
            var step = StateMachine.MovementSpeed * Time.deltaTime;
         
      
            
            var stateMachinePosition = StateMachine.transform.position;
            
            if (StateMachine.FieldOfView.CanSeePlayer)
            {
                StateMachine.SwitchState(new EnemyWarningState(StateMachine));
            }
            
            Move(_targetPosition);
            if (!(Vector3.Distance(stateMachinePosition, _targetPosition) < StateMachine.PointRadius)) return;
            
            _targetPosition = GetRoamingPosition();
            _targetWaitTime = RandomPositionHelper.GetRandomTime();

            StateMachine.SwitchState(new EnemyIdleState(StateMachine, _targetWaitTime));
        }

  
    

        public override void Exit()
        {
            StateMachine.NavMeshAgent.stoppingDistance = StateMachine.PointRadius;
        }
    }
}