using Interfaces;
using UnityEngine;

namespace Interaction
{
    public class CharacterInteraction: MonoBehaviour, IInteractable
    {
        [SerializeField] private string name;
        
        public void Interact(Transform playerPosition)
        {
            
        }

        public string GetInteractionText()
        {
            return "Talk with " + name;
        }

        public Transform GetTransform()
        {
            return transform;
        }
        
    }
}