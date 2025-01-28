using UnityEngine;

namespace Enemy.Health.FollowTarget
{
    public class FollowTarget: MonoBehaviour
    {
        private Transform target;
        private Vector3 offset;

        public Transform Target
        {
            set => target = value;
        }
        public Vector3 Offset
        {
            set => offset = value;
        }
        private void Update()
        {
            if(!target) return;
            transform.position = target.position + offset;
        }
    }
}