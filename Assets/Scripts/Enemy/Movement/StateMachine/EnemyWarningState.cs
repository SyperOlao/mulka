using Common.Enums;
using Common.StateMachine;
using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyWarningState : EnemyBaseState
    {
        private const float TimeToSwitchState = 3f;
        private float _timerToForget = TimeToSwitchState;

        public EnemyWarningState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
        }

        public override void Tick()
        {
            SwitchStateByTime();
        }
        


        private void SwitchStateByTime()
        {
            if (!StateMachine.FieldOfView.CanSeePlayer)
            {
                StopWalking();
                StateMachine.Animator.SetBool(EnemyAnimatorEnum.Looking,true);
                _timerToForget -= Time.deltaTime;
                if (_timerToForget <= 0)
                {
                    StateMachine.SwitchState(new EnemyIdleState(StateMachine, 3f));
                }
            }
            else
            {
                 StateMachine.SwitchState(new EnemyAngryState(StateMachine));
            }
        }
        

        public override void Exit()
        {
            StateMachine.Animator.SetBool(EnemyAnimatorEnum.Looking,false);
        }
    }
}