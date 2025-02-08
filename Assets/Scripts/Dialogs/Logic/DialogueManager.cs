using System.Collections;
using System.Collections.Generic;
using Dialogs.Data.Character;
using Dialogs.Data.DialogData;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs.Logic
{
    public class DialogueManager: MonoBehaviour
    {
        private readonly int _isOpenHash = Animator.StringToHash("IsOpen");
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Animator animator;
        
        [ItemCanBeNull] private Queue<DialogDataDto> _currentDialogue;
        [ItemCanBeNull] private Queue<string> _sentences;
        private LoadedCharacterService _dialogCharacterList;
        public void Start () {
            _dialogCharacterList = new LoadedCharacterService();

        }
        
        
        public void StartDialogue(List<DialogDataDto> dialogue)
        {
            animator.SetBool(_isOpenHash, true);
            foreach (var replica in dialogue)
            {
                Debug.Log(replica.subject);
                _currentDialogue.Enqueue(replica);
                CharacterReplica(replica);
            }
            EndDialogue();
        }

        private void CharacterReplica(DialogDataDto dialogue)
        {
            var character = _dialogCharacterList.GetCharacterByName(dialogue.subject);
            nameText.text = character.Name;

            _currentDialogue.Clear();

            foreach (var sentence in dialogue.text)
            {
                _sentences.Enqueue(sentence);
            }
            
            DisplayNextSentence();
        }
        
        public void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                var dialogDataDto = _currentDialogue.Dequeue();
                _sentences = dialogDataDto.text;
                var character = _dialogCharacterList.GetCharacterByName(dialogDataDto.subject);
                nameText.text = character.Name;
            }
            
            EndDialogue();


            var sentence = _sentences.Dequeue();
            
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        
       private IEnumerator TypeSentence (string sentence)
        {
            dialogueText.text = "";
            foreach (var letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        private void EndDialogue()
        {
            animator.SetBool(_isOpenHash, false);
        }
        
    }
}