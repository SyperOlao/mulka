using Common.StateMachine;
using UnityEngine;

namespace Player.Movement.StateMachine
{
    public abstract class PlayerBaseState: State
    {
        protected readonly PlayerMoveStateMachine MoveStateMachine;
        
        protected PlayerBaseState(PlayerMoveStateMachine moveStateMachine)
        {
            MoveStateMachine = moveStateMachine;
        }

        protected void CalculateMoveDirection()
        {
            var mainCameraTransform = MoveStateMachine.MainCamera;
            Vector3 cameraForward = new(MoveStateMachine.MainCamera.forward.x, 0, mainCameraTransform.forward.z);
            Vector3 cameraRight = new(MoveStateMachine.MainCamera.right.x, 0, mainCameraTransform.right.z);

            var moveDirection = cameraForward.normalized * MoveStateMachine.InputReader.MoveComposite.y + cameraRight.normalized * MoveStateMachine.InputReader.MoveComposite.x;

            MoveStateMachine.Velocity.x = moveDirection.x * MoveStateMachine.MovementSpeed;
            MoveStateMachine.Velocity.z = moveDirection.z * MoveStateMachine.MovementSpeed;
        }
        
      
        
        protected bool CheckCollisionOverlap(Vector3 point)
        {
         
            return MoveStateMachine.Controller.isGrounded;
        }
        
        
        protected void FaceMoveDirection()
        {
            Vector3 faceDirection = new(MoveStateMachine.Velocity.x, 0f, MoveStateMachine.Velocity.z);

            if (faceDirection == Vector3.zero)
                return;

            MoveStateMachine.transform.rotation = Quaternion.Slerp(MoveStateMachine.transform.rotation, Quaternion.LookRotation(faceDirection), MoveStateMachine.LookRotationDampFactor * Time.deltaTime);
        }
        
        protected void Move(int acceleration = 1)
        {
            var deltaAcceleration = MoveStateMachine.Velocity * acceleration;
            MoveStateMachine.Controller.Move(deltaAcceleration * Time.deltaTime);
        }
    }
}