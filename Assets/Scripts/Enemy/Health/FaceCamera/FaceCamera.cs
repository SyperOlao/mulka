using UnityEngine;


namespace Enemy.Health.FaceCamera
{
    public class FaceCamera : MonoBehaviour
    {
        [SerializeField] private Camera camera1;
        public Camera Camera
        {
            set => camera1 = value;
        }

        private void Update()
        {
            transform.LookAt(camera1.transform, Vector3.up);
        }
    }
}