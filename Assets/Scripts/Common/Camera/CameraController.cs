using UnityEngine;

namespace Common.Camera
{
    public class CameraController: MonoBehaviour
    {
        
        public Transform player; // Ссылка на игрока
        public float rotationSpeed = 100f; // Скорость вращения
        public float zoomSpeed = 2f; // Скорость приближения
        public float minZoom = 5f; // Минимальное расстояние камеры
        public float maxZoom = 20f; // Максимальное расстояние

        private Vector3 offset; // Смещение камеры
        private float currentZoom = 10f; // Текущий зум
        private float currentRotation = 0f; // Текущий угол вращения

        void Start()
        {
            offset = new Vector3(0, currentZoom, 0);
        }

        void Update()
        {
            // Управление зумом колесиком мыши
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            currentZoom -= scroll * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
            offset.y = currentZoom;

            // Вращение камеры правой кнопкой мыши
            if (Input.GetMouseButton(1))
            {
                currentRotation += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            }

            // Обновление позиции и вращения камеры
            transform.position = player.position + Quaternion.Euler(0, currentRotation, 0) * offset;
            transform.LookAt(player.position);
        }
    }
}