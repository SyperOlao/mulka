using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyIdleState: EnemyBaseState
    {
        private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int _moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private float _timer;
        private const float RotationAngle = 70.0f;
        private const float RotationSpeed = 2.0f; 
        private readonly Vector3 _rotationAxis = Vector3.up; 
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;
        
        private Quaternion _originalRotation;
        private Quaternion _targetRotation;
        private bool _rotatingToTarget = true;

        public EnemyIdleState(EnemyStateMachine stateMachine, float timer = 0.0f) : base(stateMachine)
        {
            _timer = timer;
            StateMachine.Animator.CrossFadeInFixedTime(_moveBlendTreeHash, CrossFadeDuration);
        }

        public override void Enter()
        {
            _originalRotation = StateMachine.transform.rotation;
            _targetRotation = Quaternion.Euler(_rotationAxis * RotationAngle) * _originalRotation;
            StateMachine.NavMeshAgent.isStopped = true;
        }

        private void AnimateIdle()
        {
            StateMachine.Animator.SetFloat(_moveSpeedHash, 0f, AnimationDampTime,  Time.deltaTime);
        }

        public override void Tick()
        {                 
            _timer -= Time.deltaTime;
            AnimateIdle();
            LookAround();
            
            if (StateMachine.FieldOfView.CanSeePlayer)
            {
                StateMachine.SwitchState(new EnemyWarningState(StateMachine));
            }
            
            if (_timer <= 0)
            {
                StateMachine.SwitchState(new EnemyMoveState(StateMachine));
            }
        }

        private void LookAround()
        {
            if (_rotatingToTarget)
            {
                StateMachine.transform.rotation = Quaternion.Lerp(StateMachine.transform.rotation, _targetRotation, Time.deltaTime * RotationSpeed);
                if (Quaternion.Angle(StateMachine.transform.rotation, _targetRotation) < 0.1f)
                {
                   _rotatingToTarget = false;
                }
            }
            else
            {
                StateMachine.transform.rotation = Quaternion.Lerp(StateMachine.transform.rotation, _originalRotation, Time.deltaTime * RotationSpeed);
                if (Quaternion.Angle(StateMachine.transform.rotation, _originalRotation) < 0.1f)
                {
                    _rotatingToTarget = true;
                }
            }
        }

        public override void Exit()
        {
            StateMachine.NavMeshAgent.isStopped = false; 
        }
    }
}