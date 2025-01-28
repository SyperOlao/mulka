using System;
using System.Collections;
using UnityEngine;

namespace Enemy.FOV
{
    public class FieldOfView : MonoBehaviour
    {
        public bool CanSeePlayer { get; private set; }
        [Range(0, 360)] public float Angle;
        public float Radius;
        public GameObject PlayerRef;
        public event Action IsPlayerVisible;

        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _obstructionMask;

        private readonly Collider[] _results = new Collider[1];
        private const float Delay = 0.2f;
        private bool _previousPlayerVisible;

        public Vector3 LastPlayerPosition { get; private set; }
        public Vector3 PlayerPositionInCircle { get; private set; }
        public bool IsPlayerPositionInCircle { get; private set; }
        private void Start()
        {
            PlayerRef = GameObject.Find(nameof(Player));
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            var wait = new WaitForSeconds(Delay);
            while (true)
            {
                yield return wait;
                CanSeePlayer = FieldOfViewCheck();
                if (CanSeePlayer != _previousPlayerVisible)
                {
                    IsPlayerVisible?.Invoke();
                }
                _previousPlayerVisible = CanSeePlayer;
            }
            
        }
        // TODO:: переписать 
        private bool FieldOfViewCheck() 
        {
            var currentPosition = transform.position;
            var count = Physics.OverlapSphereNonAlloc(currentPosition, Radius, _results, _targetMask);
            IsPlayerPositionInCircle = false;
            if (count <= 0) return false;
            IsPlayerPositionInCircle = true;
            var target = _results[0].transform;
            var targetPosition = target.position;
            var directionToTarget = (targetPosition - currentPosition).normalized;
            PlayerPositionInCircle = targetPosition;
            if (!(Vector3.Angle(transform.forward, directionToTarget) < Angle / 2)) return false;

            var distanceToTarget = Vector3.Distance(currentPosition, targetPosition);
            var canSeePlayer =  !Physics.Raycast(currentPosition, directionToTarget, distanceToTarget, _obstructionMask);
            if (canSeePlayer)
            {
                LastPlayerPosition = targetPosition;
            }

            return canSeePlayer;
        }
    }
}