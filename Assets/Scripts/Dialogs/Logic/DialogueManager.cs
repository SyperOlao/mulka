using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogs.Data.Character;
using Dialogs.Data.DialogData;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs.Logic
{
    public class DialogueManager : MonoBehaviour
    {
        private readonly int _isOpenHash = Animator.StringToHash("IsOpen");
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Animator animator;
        [SerializeField] private Image image;

        [ItemCanBeNull] private Queue<DialogDataDto> _currentDialogue;
        [ItemCanBeNull] private Queue<string> _sentences;
        private LoadedCharacterService _dialogCharacterList;

        public void Start()
        {
            _dialogCharacterList = new LoadedCharacterService();
            _currentDialogue = new Queue<DialogDataDto>();
            _sentences = new Queue<string>();
        }

        public void StartDialogue(List<DialogDataDto> dialogue)
        {
            animator.SetBool(_isOpenHash, true);
            foreach (var replica in dialogue)
            {
                _currentDialogue.Enqueue(replica);
            }

            CharacterReplica();
        }

        private void CharacterReplica()
        {
            var dialogue = _currentDialogue.Dequeue();
            ChangeSpeaker(dialogue);
            DisplayNextSentence();
        }

        private void ChangeSpeaker(DialogDataDto dialogDataDto)
        {
            var character = _dialogCharacterList.GetCharacterByName(dialogDataDto.subject);
            nameText.text = character.Name;
            Debug.Log(character.PngPath);
            var sp = Resources.Load<Sprite>(character.PngPath);
            Debug.Log(sp);
            image.sprite = Resources.Load<Sprite>(character.PngPath);
            _sentences.Clear();
            foreach (var sentence in dialogDataDto.text)
            {
                _sentences.Enqueue(sentence);
            }

        }

        public void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                if (_currentDialogue.TryDequeue(out var dialogDataDto))
                {
                    ChangeSpeaker(dialogDataDto);
                }
                else
                {
                    EndDialogue();
                    return;
                }
            }

            var sentence = _sentences.Dequeue();

            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        private IEnumerator TypeSentence(string sentence)
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
            dialogueText.text = "ENDED!!!";
            animator.SetBool(_isOpenHash, false);
        }
    }
}