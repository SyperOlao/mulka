using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyMoveState : EnemyBaseState
    {
        private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int _moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;


        public EnemyMoveState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
     
            StateMachine.Velocity.y = Physics.gravity.y;

            StateMachine.Animator.CrossFadeInFixedTime(_moveBlendTreeHash, CrossFadeDuration);
        }

        public override void Tick()
        {
            var step = StateMachine.MovementSpeed * Time.deltaTime;
            if (StateMachine.Points.Count <= 0)
            {
                StateMachine.SwitchState(new EnemyIdleState(StateMachine, float.PositiveInfinity));
                return;
            }
            var targetPosition = StateMachine.Points[StateMachine.CurrentPoint].point;
            var targetTime = StateMachine.Points[StateMachine.CurrentPoint].time;
            var stateMachinePosition = StateMachine.transform.position;
            Move(stateMachinePosition, targetPosition, step);
            FaceToDirection(targetPosition, stateMachinePosition);
            if (!(Vector3.Distance(stateMachinePosition, targetPosition) < StateMachine.PointRadius)) return;
            
            ChangeCurrentPoint();
            StateMachine.SwitchState(new EnemyIdleState(StateMachine, targetTime));
        }

        public override void Exit()
        {
        }
    }
}