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
            Debug.Log("EnemyAttackState");
        }

        public override void Tick()
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
      
            Debug.Log(StateMachine.FieldOfView.CanSeePlayer);
            if (!StateMachine.FieldOfView.CanSeePlayer)
            {
                Debug.Log("Timer " + _timer);
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