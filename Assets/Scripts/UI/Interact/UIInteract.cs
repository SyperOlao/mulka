using System;
using Interfaces;
using JetBrains.Annotations;
using Player.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Interact
{
    public class UIInteract : MonoBehaviour
    {
        [SerializeField] private GameObject container;
        [SerializeField] private PlayerInteract playerInteract;
        [SerializeField] private TextMeshProUGUI interactTextMesh;

        private int _frameCounter;
        private const int UpdateInterval = 10;
        private Camera _mainCamera;
        private IInteractable _interactable;
        private bool _isCameraAssign;
        public void Awake()
        {
            Hide();
            _mainCamera = Camera.main;
            _isCameraAssign = true;
        }

        private void Show(IInteractable interactable)
        {
            container.SetActive(true);
            interactTextMesh.text = interactable.GetInteractionText();
            SetInteractableNamePosition(interactable);
        }

        private void Hide()
        {
            container.SetActive(false);
        }

        public void Update()
        {
             Interaction(_interactable);
        }

        public void FixedUpdate()
        {
            _frameCounter++;
            if (_frameCounter < UpdateInterval) return;
            
            _frameCounter = 0;
            _interactable = playerInteract.GetInteractionObject();
        }

        private void Interaction([CanBeNull] IInteractable interactable)
        {
            if (interactable == null)
            {
                Hide();
            }
            else
            {
                Show(interactable);
            }
        }

        private void SetInteractableNamePosition(IInteractable interactable)
        {
            if (!_isCameraAssign) return;
            var worldPosition = interactable.GetTransform().position + Vector3.up;

            var screenPosition = _mainCamera.WorldToScreenPoint(worldPosition);

            container.transform.position = screenPosition;
        }
    }
}