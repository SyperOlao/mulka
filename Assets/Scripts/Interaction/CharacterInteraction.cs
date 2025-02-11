using Interfaces;
using UnityEngine;

namespace Interaction
{
    public class CharacterInteraction: MonoBehaviour, IInteractable
    {
        
        
        public void Interact(Transform playerPosition)
        {
            
        }

        public string GetInteractionText()
        {
            return "PASDDSAD";
        }

        public Transform GetTransform()
        {
            return transform;
        }
        
    }
}