using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyIdleState: EnemyBaseState
    {
        private float _timer;
        private float _rotationAngle = 70.0f; 
        private float _rotationSpeed = 2.0f; 
        private Vector3 _rotationAxis = Vector3.up; 

        
        private Quaternion _originalRotation;
        private Quaternion _targetRotation;
        private bool _rotatingToTarget = true;

        public EnemyIdleState(EnemyStateMachine stateMachine, float timer = 0.0f) : base(stateMachine)
        {
            _timer = timer;
        }

        public override void Enter()
        {
            var playerPosition = StateMachine.FieldOfView.LastPlayerPosition;
            FaceToDirection( playerPosition,  StateMachine.transform.position);
            _originalRotation = StateMachine.transform.rotation;
            _targetRotation = Quaternion.Euler(_rotationAxis * _rotationAngle) * _originalRotation;
        }

        public override void Tick()
        {                 
            _timer -= Time.deltaTime;
            LookAround();
            if (_timer <= 0)
            {
                StateMachine.SwitchState(new EnemyMoveState(StateMachine));
            }
        }

        private void LookAround()
        {
            if (_rotatingToTarget)
            {
                StateMachine.transform.rotation = Quaternion.Lerp(StateMachine.transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);
                if (Quaternion.Angle(StateMachine.transform.rotation, _targetRotation) < 0.1f)
                {
                   _rotatingToTarget = false;
                }
            }
            else
            {
                StateMachine.transform.rotation = Quaternion.Lerp(StateMachine.transform.rotation, _originalRotation, Time.deltaTime * _rotationSpeed);
                if (Quaternion.Angle(StateMachine.transform.rotation, _originalRotation) < 0.1f)
                {
                    _rotatingToTarget = true;
                }
            }
        }

        public override void Exit()
        {
        }
    }
}