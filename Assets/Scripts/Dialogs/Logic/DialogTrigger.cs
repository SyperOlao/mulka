using System;
using System.Collections.Generic;
using Dialogs.Data.DialogData;
using Newtonsoft.Json;
using UI.Dialog;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Dialogs.Logic
{
    public class DialogTrigger : MonoBehaviour
    {
        [SerializeField] private TextAsset jsonFile;
        
        private List<DialogDataDto> _dialogue;
        private DialogueManager _dialogueManager;
        
        public void Awake()
        {
            _dialogueManager = FindObjectOfType<DialogueManager>();
        }

        public void TriggerDialogue()
        {
            if (jsonFile == null)
            {
                throw new Exception("Json in DialogTrigger does not exists");
            }
            _dialogue = JsonConvert.DeserializeObject<List<DialogDataDto>>(jsonFile.text);
            
            
            _dialogueManager.StartDialogue(_dialogue);
        }

        public void EndDialogue()
        {
            _dialogueManager.EndDialogue();
        }
    }
}