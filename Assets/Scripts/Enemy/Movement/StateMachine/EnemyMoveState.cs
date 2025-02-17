using Common.Utils;
using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyMoveState : EnemyBaseState
    {
        private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int _moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float AnimationDampTime = 0.1f;
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
            
            AnimateWalk();
            Move(stateMachinePosition, _targetPosition, step);
            FaceToDirection(_targetPosition, stateMachinePosition);
            if (!(Vector3.Distance(stateMachinePosition, _targetPosition) < StateMachine.PointRadius)) return;
            
            _targetPosition = GetRoamingPosition();
            _targetWaitTime = RandomPositionHelper.GetRandomTime();
        
            StateMachine.SwitchState(new EnemyIdleState(StateMachine, _targetWaitTime));
        }

        private void AnimateWalk()
        {
            StateMachine.Animator.SetFloat(_moveSpeedHash, 1f, AnimationDampTime,  Time.deltaTime);
        }

        public override void Exit()
        {
        }
    }
}