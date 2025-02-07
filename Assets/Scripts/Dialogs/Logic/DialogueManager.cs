using System.Collections;
using System.Collections.Generic;
using Dialogs.Data.Character;
using Dialogs.Data.SpeechText;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs.Logic
{
    public class DialogueManager: MonoBehaviour
    {
        private readonly int _isOpenHash = Animator.StringToHash("IsOpen");
        [SerializeField] private Text nameText;
        [SerializeField] private Text dialogueText;
        
        [SerializeField] private Queue<DialogDataDto> dialogData;
        [SerializeField] private DialogCharacterDto[] dialogCharArrayList;
        [SerializeField] private Animator animator;
        
        private Queue<string> _sentences;
        
        public void Start () {
            _sentences = new Queue<string>();
        }
        
        
        public void StartDialogue(DialogDataDto dialogue)
        {
            animator.SetBool(_isOpenHash, true);

            nameText.text = dialogue.subject;

            _sentences.Clear();

            foreach (var sentence in dialogue.text)
            {
                _sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }
        
        private void DisplayNextSentence ()
        {
            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

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