using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class PlayerJumpState : PlayerBaseState
    {
        private readonly int _jumpBlendTreeHash = Animator.StringToHash("Jump");
        private const float CrossFadeDuration = 0.1f;
        private const float TimeToJump = 1f;
        private bool _isGrounded;
        private static float _jumpForce = 3f;

        public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log("JUMP!!!!");
            FaceMoveDirection();
            // StateMachine.Animator.CrossFadeInFixedTime(_jumpBlendTreeHash, CrossFadeDuration);
            JumpCalculation();
            _isGrounded = false;
        }

        public override void Tick()
        {
            // StateMachine.Animator.SetFloat(_jumpBlendTreeHash,
            //     StateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 0.9f : 0f, 0.9f,
            //     Time.deltaTime);
            
            ApplyGravity();
            Debug.Log(_isGrounded);
            if (_isGrounded)
            {
                StateMachine.SwitchState(new PlayerMoveState(StateMachine));
                return;
            }
            PhysicsUpdate();
        }


        private void PhysicsUpdate()
        {
            CalculateMoveDirection();
            FaceMoveDirection();
            _isGrounded = CheckCollisionOverlap(StateMachine.transform.position);
        }
        
        private void ApplyGravity()
        {
            if (_isGrounded) return;
            StateMachine.Velocity.y += Physics.gravity.y * Time.deltaTime;
           // Debug.Log(StateMachine.Velocity.y);
            StateMachine.Controller.Move(StateMachine.Velocity * Time.deltaTime); 
        }

        private void JumpCalculation()
        {
             // StateMachine.transform.Translate(Vector3.up * (StateMachine.CollisionOverlapRadius + 0.1f));
             // StateMachine.ApplyImpulse(Vector3.up * _jumpForce);
            StateMachine.Velocity.y = _jumpForce;
        }

        public override void Exit()
        {
            
        }
    }
}