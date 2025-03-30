using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyAttackState: EnemyBaseState
    {
        private float _timer = 3f;
        public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
        }

        public override void Tick()
        {
            MoveToPlayer();
            if (!StateMachine.FieldOfView.CanSeePlayer) return;
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                StateMachine.SwitchState(new EnemyWarningState(StateMachine));
            }
        }
        
        private void MoveToPlayer()
        {
            var playerPositionInCircle = StateMachine.FieldOfView.playerRef.transform.position;
            Move(playerPositionInCircle);
        }

        public override void Exit()
        {
           
        }
    }
}