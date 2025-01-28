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
            if (StateMachine.FieldOfView.IsPlayerPositionInCircle) return;
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                StateMachine.SwitchState(new EnemyWarningState(StateMachine));
            }
        }
        
        private void MoveToPlayer()
        {
            var step = StateMachine.MovementSpeed * Time.deltaTime * 0.4f;
            var position = StateMachine.transform.position;
            var playerPositionInCircle = StateMachine.FieldOfView.PlayerPositionInCircle;
            Move(position, playerPositionInCircle,
                step);
            FaceToDirection( playerPositionInCircle, position);
        }

        public override void Exit()
        {
           
        }
    }
}