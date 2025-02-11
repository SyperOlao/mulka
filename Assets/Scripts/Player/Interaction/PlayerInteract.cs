using System;
using System.Collections.Generic;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Interaction
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private float interactRange = 4f;

        private PlayerInput _playerInput;
        private InputAction _interactAction;
        private readonly Collider[] _colliders = new Collider[10];
        private bool _isInteract;
 

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _interactAction = _playerInput.actions["Interact"];
        }


        public void Update()
        {
            if (!_interactAction.IsPressed()) return;
            var interactable = GetInteractionObject();
            interactable?.Interact(transform);
        }
        

        public IInteractable GetInteractionObject()
        {
            var numColliders = Physics.OverlapSphereNonAlloc(transform.position, interactRange, _colliders);
            if (numColliders <= 0) return null;
            var closestDistance = float.MaxValue;
            IInteractable closestInteractable = null;
            var position = transform.position;
            for (var i = 0; i < numColliders; i++)
            {
                if (!_colliders[i].TryGetComponent(out IInteractable newInteractable)) continue;
                var distance = Vector3.SqrMagnitude(position - newInteractable.GetTransform().position);
                if (!(distance < closestDistance)) continue;
                closestDistance = distance;
                closestInteractable = newInteractable;
            }


            return closestInteractable;
        }
    }
}