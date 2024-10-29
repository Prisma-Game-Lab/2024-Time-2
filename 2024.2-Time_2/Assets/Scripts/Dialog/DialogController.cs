using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogController : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private TMP_Text characterNameText;

    [SerializeField] private float timeBetweenLetters;

    private UnityEvent[] onCharacterChange;
    private UnityEvent onDialogEnd;

    int currentIndex;
    int currentEventIndex;
    int currentTextSize;
    int currentNameSize;
    string[] currentText;
    string[] currentName;

    private bool onDialog;

    public void StartDialog(string[] currentDialogText, string[] currentCharacterName, UnityEvent[] characterChangeEvents, UnityEvent onDialogEndEvent) 
    {
        if (onDialog)
        {
            return;
        }
        currentText = currentDialogText;
        currentTextSize = currentDialogText.Length;
        currentNameSize = currentCharacterName.Length;
        currentName = currentCharacterName;
        onCharacterChange = characterChangeEvents;
        onDialogEnd = onDialogEndEvent;
        dialogBox.SetActive(true);
        currentIndex = 0;
        currentEventIndex = 0;
        AdvanceDialog();
        onDialog = true;
    }

    public void AdvanceDialog() 
    {
        if (currentIndex < currentTextSize) 
        {
            StopAllCoroutines();
            if (currentIndex < currentNameSize)
            {
                if (currentIndex == 0 || currentName[currentIndex] != currentName[currentIndex - 1]) 
                {
                    OnCharacterChange();
                }
            }
            StartCoroutine(TypeSentence());
            currentIndex++;
        }
        else
        {
            EndDialog();
        }
    }

    private void OnCharacterChange() 
    {
        characterNameText.text = currentName[currentIndex];
        onCharacterChange[currentEventIndex]?.Invoke();
        currentEventIndex = (currentEventIndex+1) % onCharacterChange.Length;
    }

    private IEnumerator TypeSentence() 
    {
        dialogText.text = "";
        foreach (char letter in currentText[currentIndex].ToCharArray()) 
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
    } 

    private void EndDialog()
    {
        dialogBox.SetActive(false);
        onDialog = false;
        onDialogEnd?.Invoke();
    }
}
