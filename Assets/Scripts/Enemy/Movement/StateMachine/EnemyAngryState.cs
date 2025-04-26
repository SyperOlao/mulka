using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Enemy.Movement.StateMachine
{
    public class EnemyAngryState : EnemyBaseState
    {
        private const float TimeToForget = 3f;
        private float _timer = 3f;

        private const float AttackDuration = 1f;
        private float _timerAttack = AttackDuration;
        private bool _isInAttackPhase = true;
        private const float CooldownDuration = 5f;
        private float _timerCooldown;
        public EnemyAngryState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
        }

        public override void Tick()
        {
            Behaviour();
        }


        private void Behaviour()
        {
            if (!IsPlayerInDistance(DistanceToAttack))
            {
                MoveToPlayer();
            }
            else
            {
                StopWalking();

                StateMachine.SwitchState(new EnemyAttackState(StateMachine));
            }

            HandelForgetPlayer();
        }
        

        private void HandelForgetPlayer()
        {
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

            LookAt(StateMachine.FieldOfView.playerRef.transform.position);
        }

        private void MoveToPlayer()
        {
            var playerPos = StateMachine.PlayerTransform.position;
            Debug.Log(playerPos.x + playerPos.y + playerPos.z);
            
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