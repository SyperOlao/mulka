using Dialogs.Logic;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Interaction
{
    public class CharacterInteraction: MonoBehaviour, IInteractable
    {
        [SerializeField] private string characterName = "";
        [SerializeField] [CanBeNull] private DialogTrigger dialogTrigger;

        public void Interact(Transform playerPosition)
        {
            if (!ReferenceEquals(dialogTrigger, null))
            {
                dialogTrigger.TriggerDialogue();
            }
        }

        public string GetInteractionText()
        {
            return "Talk with " + characterName;
        }

        public Transform GetTransform()
        {
            return transform;
        }
        
    }
}