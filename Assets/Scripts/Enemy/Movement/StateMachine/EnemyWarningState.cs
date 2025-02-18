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
            var lastPlayerPosition = StateMachine.FieldOfView.playerRef.transform.position;
            Move(lastPlayerPosition);
        }
        

        private void SwitchStateByTime()
        {
            if (!StateMachine.FieldOfView.CanSeePlayer)
            {
                _timerToForget -= Time.deltaTime;
                if (_timerToForget <= 0)
                {
                    StateMachine.SwitchState(new EnemyIdleState(StateMachine, 3f));
                }
                _timer = TimeToSwitchState;
            }
            // else
            // {
            //     _timer -= Time.deltaTime;
            //     if (_timer <= 0)
            //     {
            //         StateMachine.SwitchState(new EnemyAttackState(StateMachine));
            //     }
            //     _timerToForget = TimeToSwitchState;
            // }
        }

        public override void Exit()
        {
            
        }
    }
}