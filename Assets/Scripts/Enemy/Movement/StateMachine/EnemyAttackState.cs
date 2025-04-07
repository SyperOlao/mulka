using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyAttackState : EnemyBaseState
    {
        private const float TimeToForget = 3f;
        private float _timer = 3f;

        public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
        }

        public override void Tick()
        {
            MoveBehaviour();
        }
        
        

        private void MoveBehaviour()
        {
            if (!IsPlayerInDistance(DistanceToAttack))
            {
                MoveToPlayer();
            }
            else
            {
                StopWalking();
            }

            LookAt(StateMachine.FieldOfView.playerRef.transform.position);

            if (!StateMachine.FieldOfView.CanSeePlayer)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    StateMachine.SwitchState(new EnemyWarningState(StateMachine));
                }
            }
            else
            {
                _timer = TimeToForget;
            }
        }

        private void MoveToPlayer()
        {
            var playerPos = StateMachine.FieldOfView.playerRef.transform.position;
            var enemyPos = StateMachine.transform.position;

            var directionToEnemy = (enemyPos - playerPos).normalized;
            var targetPositionOnCircle = playerPos + directionToEnemy * DistanceToAttack;
            LookAt(playerPos);
            Move(targetPositionOnCircle);
        }

        public override void Exit()
        {
        }
    }
}