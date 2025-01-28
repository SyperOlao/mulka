using Common.StateMachine;
using UnityEngine;

namespace Player.Movement.StateMachine
{
    public abstract class PlayerBaseState: State
    {
        protected readonly PlayerStateMachine StateMachine;
        
        protected PlayerBaseState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected void CalculateMoveDirection()
        {
            var mainCameraTransform = StateMachine.MainCamera;
            Vector3 cameraForward = new(StateMachine.MainCamera.forward.x, 0, mainCameraTransform.forward.z);
            Vector3 cameraRight = new(StateMachine.MainCamera.right.x, 0, mainCameraTransform.right.z);

            var moveDirection = cameraForward.normalized * StateMachine.InputReader.MoveComposite.y + cameraRight.normalized * StateMachine.InputReader.MoveComposite.x;

            StateMachine.Velocity.x = moveDirection.x * StateMachine.MovementSpeed;
            StateMachine.Velocity.z = moveDirection.z * StateMachine.MovementSpeed;
        }

        protected void FaceMoveDirection()
        {
            Vector3 faceDirection = new(StateMachine.Velocity.x, 0f, StateMachine.Velocity.z);

            if (faceDirection == Vector3.zero)
                return;

            StateMachine.transform.rotation = Quaternion.Slerp(StateMachine.transform.rotation, Quaternion.LookRotation(faceDirection), StateMachine.LookRotationDampFactor * Time.deltaTime);
        }
        
        protected void Move()
        {
            StateMachine.Controller.Move(StateMachine.Velocity * Time.deltaTime);
        }
    }
}