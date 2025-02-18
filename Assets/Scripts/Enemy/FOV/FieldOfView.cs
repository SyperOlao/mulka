using System;
using System.Collections;
using Common.Utils;
using UnityEngine;

namespace Enemy.FOV
{
    public class FieldOfView : MonoBehaviour
    {
        
        [Header("FOV Settings")]
        [SerializeField, Range(0, 360)] private float angle;
        [SerializeField] private float radius;
        [SerializeField] public GameObject playerRef;
    
        [Header("References")]
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private LayerMask obstructionMask;
        
        public bool CanSeePlayer { get; private set; }
        
        private const float Delay = 0.2f;
        private bool _previousPlayerVisible;
        private readonly Collider[] _targetsInViewRadius = new Collider[10];
        
        public Transform PlayerTransform { get; private set; }
        public Transform LastPlayerTransform { get; private set; }
        private void Start()
        {
            playerRef = GameObject.Find(nameof(Player));
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Delay); 
                FieldOfViewCheck();
            }
        }
        
        private void FieldOfViewCheck()
        {
            CanSeePlayer = false;

            var targetsCount = Physics.OverlapSphereNonAlloc(transform.position, radius, _targetsInViewRadius, targetMask);
           
            
            for (var i = 0; i < targetsCount; i++)
            {
                var targetTransform = _targetsInViewRadius[i].transform;
                var directionToTarget = (targetTransform.position - transform.position).normalized;

                
                if (!(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)) continue;
                var distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
                    
                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    continue;
                CanSeePlayer = true;
                LastPlayerTransform = targetTransform;
                DebugHelper.Dot(LastPlayerTransform.position);
                return;
            }
        }
        
        
    }
}