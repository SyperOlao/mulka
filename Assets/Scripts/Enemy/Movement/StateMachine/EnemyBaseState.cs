using System;
using Common.Utils;
using Unity.VisualScripting;
using UnityEngine;
using State = Common.StateMachine.State;

namespace Enemy.Movement.StateMachine
{
    public abstract class EnemyBaseState : State
    {
        protected readonly EnemyStateMachine StateMachine;

        private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
        private const float AnimationDampTime = 0.1f;
        protected float DistanceToAttack { get; } = 3f;

        protected EnemyBaseState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected void LookAt(Vector3 position)
        {
            var direction = position - StateMachine.transform.position;
            direction.y = 0;

            StateMachine.transform.rotation = Quaternion.LookRotation(direction);
        }

        protected void Move(Vector3 endPosition)
        {
            StateMachine.NavMeshAgent.SetDestination(endPosition);
            LookAt(endPosition);
            AnimateWalk();
        }

        protected void StopWalking()
        {
            StateMachine.NavMeshAgent.SetDestination(StateMachine.transform.position);
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
                StateMachine.FieldOfView.playerRef.transform.position) <= distanceToAttack;
        }
    }
}