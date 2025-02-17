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

        protected EnemyBaseState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }


        protected void Move(Vector3 startPosition, Vector3 endPosition, float step)
        {
            StateMachine.transform.position = Vector3.MoveTowards(startPosition, endPosition, step);
        }

        protected void FaceToDirection(Vector3 targetPosition, Vector3 followerPosition)
        {
            if (targetPosition == Vector3.zero) return;
            var directionToTarget = (targetPosition - followerPosition).normalized;
            StateMachine.transform.rotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
            
        }

        protected Vector3 GetRoamingPosition()
        {
            return RandomPositionHelper.GetRandomPosition(StateMachine.RadiusStartFiled, StateMachine.StartPosition);
        }
    }
}