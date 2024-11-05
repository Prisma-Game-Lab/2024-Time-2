using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class DialogController : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private TMP_Text characterNameText;

    [SerializeField] private float timeBetweenLetters;

    private string txtPath = "";
    private UnityEvent[] onCharacterChange;
    private UnityEvent onDialogEnd;

    int currentIndex;
    int currentEventIndex;
    int currentDialogSize;
    string[] fileLines;
    string currentName;

    private bool onDialog;
    private bool writingLine;

    public void StartDialog(string txtFileName, UnityEvent[] characterChangeEvents, UnityEvent onDialogEndEvent) 
    {
        if (onDialog)
        {
            return;
        }
        txtPath = Application.dataPath + "/Dialogues/" + txtFileName + ".txt";
        fileLines = File.ReadAllLines(txtPath);
        currentDialogSize = fileLines.Length;
        onCharacterChange = characterChangeEvents;
        onDialogEnd = onDialogEndEvent;
        dialogBox.SetActive(true);
        currentName = "";
        currentIndex = 0;
        currentEventIndex = 0;
        AdvanceDialog();
        onDialog = true;
    }

    public void AdvanceDialog() 
    {
        if (writingLine)
        {
            OnDialogSkip();
        }
        if (currentIndex < currentDialogSize)
        { 
            StopAllCoroutines();
            string newName = fileLines[currentIndex].Remove(fileLines[currentIndex].Length - 1);
            currentIndex++;
            if(currentName != newName) 
            {
                currentName = newName;
                OnCharacterChange();
            }
            StartCoroutine(TypeSentence());
        }
        else
        {
            EndDialog();
        }
    }

    private void OnCharacterChange() 
    {
        characterNameText.text = currentName;
        onCharacterChange[currentEventIndex]?.Invoke();
        currentEventIndex = (currentEventIndex+1) % onCharacterChange.Length;
    }

    private void OnDialogSkip() 
    {
        while (currentIndex < currentDialogSize && fileLines[currentIndex] != "<end>")
        {
            currentIndex++;
        }
        currentIndex++;
        writingLine = false;
    }

    private IEnumerator TypeSentence() 
    {
        dialogText.text = "";
        writingLine = true;
        while (currentIndex < currentDialogSize && fileLines[currentIndex] != "<end>")
        {
            foreach (char letter in fileLines[currentIndex].ToCharArray()) 
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(timeBetweenLetters);
            }
            dialogText.text += '\n';
            currentIndex++;
        }
        currentIndex++;
        writingLine = false;
    } 

    private void EndDialog()
    {
        dialogBox.SetActive(false);
        onDialog = false;
        onDialogEnd?.Invoke();
    }
}
