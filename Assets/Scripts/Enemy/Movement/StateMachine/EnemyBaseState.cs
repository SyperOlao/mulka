using System;
using Common.Enums;
using Common.Utils;
using Unity.VisualScripting;
using UnityEngine;
using State = Common.StateMachine.State;

namespace Enemy.Movement.StateMachine
{
    public abstract class EnemyBaseState : State
    {
        protected readonly EnemyStateMachine StateMachine;

        private readonly int _moveSpeedHash = Animator.StringToHash(EnemyAnimatorEnum.MoveSpeed);
        private const float AnimationDampTime = 0.1f;
        protected float DistanceToAttack { get; } = 0.5f;
        
        private Quaternion _initialRotation;
 
        protected EnemyBaseState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected void LookAt(Vector3 position)
        {
            var direction = position - StateMachine.transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude < 0.0001f) 
                return; 

    
            Quaternion targetRot = Quaternion.LookRotation(direction);
            StateMachine.transform.rotation = targetRot;
            
        }

        protected void Move(Vector3 endPosition)
        {
            StateMachine.NavMeshAgent.isStopped = false;
            StateMachine.NavMeshAgent.SetDestination(endPosition);
            LookAt(endPosition);
            AnimateWalk();
        }

        protected void StopWalking()
        {
            StateMachine.NavMeshAgent.isStopped = true;
            StateMachine.NavMeshAgent.ResetPath();
            StateMachine.NavMeshAgent.velocity = Vector3.zero;
            StateMachine.Animator.SetFloat(_moveSpeedHash, 0f, AnimationDampTime, Time.deltaTime);
        }

        private void AnimateWalk()
        {
            StateMachine.Animator.SetFloat(_moveSpeedHash, 1f, AnimationDampTime, Time.deltaTime);
        }

        protected Vector3 GetRoamingPosition()
        {
            return RandomPositionHelper.GetRandomPosition(StateMachine.RadiusStartFiled, StateMachine.StartPosition);
        }

        protected bool IsPlayerInDistance(float distanceToAttack)
        {
            return Vector3.Distance(StateMachine.transform.position,
                StateMachine.PlayerTransform.position) <= distanceToAttack;
        }
    }
}