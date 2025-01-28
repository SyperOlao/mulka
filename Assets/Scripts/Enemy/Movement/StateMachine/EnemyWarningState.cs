using Common.StateMachine;
using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyWarningState: EnemyBaseState
    {
        private const float TimeToSwitchState = 3f;
        private float _timer = TimeToSwitchState;
        private float _timerToForget = TimeToSwitchState;
        
        public EnemyWarningState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {

        }

        public override void Tick()
        {
            MoveToPlayer();
            SwitchStateByTime();
        }

        private void MoveToPlayer()
        {
            var step = StateMachine.MovementSpeed * Time.deltaTime * 0.5f;
            var position = StateMachine.transform.position;
            var lastPlayerPosition = StateMachine.FieldOfView.LastPlayerPosition;
            Move(position, lastPlayerPosition,
                step);
            FaceToDirection( lastPlayerPosition, position);
        }

        private void SwitchStateByTime()
        {
            if (!StateMachine.FieldOfView.CanSeePlayer)
            {
                _timerToForget -= Time.deltaTime;
                if (_timerToForget <= 0)
                {
                    StateMachine.SwitchState(new EnemyMoveState(StateMachine));
                }
                _timer = TimeToSwitchState;
            }
            else
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    StateMachine.SwitchState(new EnemyAttackState(StateMachine));
                }
                _timerToForget = TimeToSwitchState;
            }
        }

        public override void Exit()
        {
            
        }
    }
}