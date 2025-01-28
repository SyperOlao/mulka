using UnityEngine;

namespace DataClasses
{
    [System.Serializable]
    public class PointsWithTimeStandby
    {
        public Vector3 point; 
        public float time;

        public PointsWithTimeStandby(Vector3 point, float time = 0)
        {
            this.point = point;
            this.time = time;
        }
    }
}