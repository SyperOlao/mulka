using System;
using System.Collections.Generic;
using Dialogs.Data.DialogData;
using Newtonsoft.Json;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Dialogs.Logic
{
    public class DialogTrigger : MonoBehaviour
    {
        [SerializeField] private TextAsset jsonFile;

        private List<DialogDataDto> _dialogue;


        public void TriggerDialogue()
        {
            if (jsonFile == null)
            {
                throw new Exception("Json in DialogTrigger does not exists");
            }
            _dialogue = JsonConvert.DeserializeObject<List<DialogDataDto>>(jsonFile.text);

            FindObjectOfType<DialogueManager>().StartDialogue(_dialogue);
        }
    }
}