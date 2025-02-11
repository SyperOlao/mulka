using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        void Interact(Transform playerPosition);
        
        string GetInteractionText();
        Transform GetTransform();
        
        string GetInstanceId();
    }
}