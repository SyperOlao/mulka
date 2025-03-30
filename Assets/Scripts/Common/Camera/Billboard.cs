using System;
using UnityEngine;

namespace Common.Camera
{
    public class Billboard:MonoBehaviour
    {
        [SerializeField] private Transform mainCamera;

        private void LateUpdate()
        {
            transform.LookAt(transform.position + mainCamera.forward);
        }
    }
}