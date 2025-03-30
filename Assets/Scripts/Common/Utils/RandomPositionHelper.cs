using UnityEngine;
using UnityEngine.AI;


namespace Common.Utils
{
    public static class RandomPositionHelper
    {
        public static Vector3 GetRandomPosition(int radius, Vector3 startPosition)
        {
            while (true)
            {
                var randomDirection = Random.insideUnitSphere * radius;
                randomDirection += startPosition;
                randomDirection.y = startPosition.y;

                if (NavMesh.SamplePosition(randomDirection, out var hit, radius, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
        }

        public static int GetRandomTime()
        {
            return Random.Range(1, 4);
        }
    }
}