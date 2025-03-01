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
        
        protected EnemyBaseState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        private void LookAt(Vector3 position)
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

        private void AnimateWalk()
        {
            StateMachine.Animator.SetFloat(_moveSpeedHash, 1f, AnimationDampTime, Time.deltaTime);
        }

        protected Vector3 GetRoamingPosition()
        {
            return RandomPositionHelper.GetRandomPosition(StateMachine.RadiusStartFiled, StateMachine.StartPosition);
        }
    }
}