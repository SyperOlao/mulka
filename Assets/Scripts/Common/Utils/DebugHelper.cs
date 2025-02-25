using UnityEngine;

namespace Common.Utils
{
    public static class DebugHelper
    {
      
        
        public static void DebugEndPosition(Vector3 position)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = position;
            sphere.transform.localScale = Vector3.one * 0.5f;
            Object.Destroy(sphere.GetComponent<Collider>());
            Debug.DrawRay(position, Vector3.up * 2, Color.red, 2f);
            Object.Destroy(sphere, 8);
        }

        public static void Dot(Vector3 position, Color color)
        {
            Debug.DrawRay(position, Vector3.up * 2, color, 2f);
        }
        
        public static void DebugPath(Vector3 startPosition, Vector3 endPosition)
        {
            Dot(startPosition,  Color.red);
            Debug.DrawLine(startPosition, endPosition, Color.green);
            Dot(endPosition,  Color.blue);
        }
        
        
    }
}