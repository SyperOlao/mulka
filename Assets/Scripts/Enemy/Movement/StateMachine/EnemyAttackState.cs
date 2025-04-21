using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Enemy.Movement.StateMachine
{
    public class EnemyAttackState : EnemyBaseState
    {
        private const float TimeToForget = 3f;
        private float _timer = 3f;

        private const float AttackDuration = 3f;
        private float _timerAttack = AttackDuration;
        private bool _isInAttackPhase = true;
        private const float CooldownDuration = 5f;
        private float _timerCooldown;
        const float EPS = 0.000000001f; 
        public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log("EnemyAttackState");
            _timerAttack = StateMachine.Weapon.GetClipTime();
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
                if (_isInAttackPhase)
                {
                    DoAttackLoop();
                }
                else
                {
                    _timerCooldown -= Time.deltaTime;
                    if (_timerCooldown <= 0f)
                    {
                        StateMachine.Weapon.EndAnimation();
                        StateMachine.Weapon.EndAttack();
                        _isInAttackPhase = true;
                        _timerAttack = StateMachine.Weapon.GetClipTime();
                    }
                }
            }

            HandelForgetPlayer();
        }

        private void DoAttackLoop()
        {
            if (AttackDuration - _timerAttack <= EPS)
            {
                StateMachine.Weapon.StartAnimation();
            }

            Debug.Log("Get " + StateMachine.Weapon.GetClipTime()); 
            
            
            _timerAttack -= Time.deltaTime;
            if (_timerAttack > 0)
            {
                StateMachine.Weapon.OnAttack();
            }
            else
            {
                StateMachine.Weapon.EndAttack();
                StateMachine.Weapon.EndAnimation();

                _isInAttackPhase = false;
                _timerCooldown = CooldownDuration;
            }
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