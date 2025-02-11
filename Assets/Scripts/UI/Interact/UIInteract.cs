using System;
using Interfaces;
using JetBrains.Annotations;
using Player.Interaction;
using TMPro;
using UnityEngine;

namespace UI.Interact
{
    public class UIInteract : MonoBehaviour
    {
        [SerializeField] private GameObject container;
        [SerializeField] private PlayerInteract playerInteract;
        [SerializeField] private TextMeshProUGUI interactTextMesh;
        private int _frameCounter;
        private const int UpdateInterval = 10;
        private void Show( IInteractable interactable)
        {
            container.SetActive(true);
            interactTextMesh.text = interactable.GetInteractionText();
        }

        private void Hide()
        {
            container.SetActive(false);
        }

        public void Update()
        {
            _frameCounter++;
            if (_frameCounter < UpdateInterval) return;
            
            _frameCounter = 0;
            Debug.Log("DASDSDSASDADs");
            var interact = playerInteract.GetInteractionObject();
            Interaction(interact);
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
    }
}