using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private Image[] charactersImage;
    [SerializeField] private Color blacknedColor;

    [SerializeField] private float timeBetweenLetters;

    private string txtPath = "";
    private UnityEvent[] onCharacterChangeEvent;
    private UnityEvent onDialogEnd;

    public delegate void OnDialogStart();
    public static event OnDialogStart onDialogStart;

    public delegate void OnDialogFinish();
    public static event OnDialogFinish onDialogFinish;

    int currentIndex;
    int currentEventIndex;
    int currentDialogSize;
    int characterIndex;
    string[] fileLines;
    string currentName;

    private bool onDialog;
    private bool writingLine;

    public void StartDialog(string txtFileName, UnityEvent[] characterChangeEvents, UnityEvent onDialogEndEvent, Sprite[] chrImages, bool firstSentence)
    {
        onDialogStart?.Invoke();
        if (onDialog)
        {
            return;
        }
        txtPath = Application.streamingAssetsPath + "/Dialogues/" + txtFileName + ".txt";
        fileLines = File.ReadAllLines(txtPath);
        currentDialogSize = fileLines.Length;
        onCharacterChangeEvent = characterChangeEvents;
        onDialogEnd = onDialogEndEvent;
        dialogBox.SetActive(true);
        currentName = "";
        currentIndex = 0;
        currentEventIndex = 0;

        if (firstSentence)
        {
            characterIndex = 1;
        }
        else
        {
            characterIndex = 0;
        }

        for (int i = 0; i < chrImages.Length; i++)
        {
            charactersImage[i].sprite = chrImages[i];
            charactersImage[i].preserveAspect = true;
        }

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
            if (currentName != newName)
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
        speakerNameText.text = currentName;
        onCharacterChangeEvent[currentEventIndex]?.Invoke();
        charactersImage[characterIndex].color = new Color(255,255,255);

        characterIndex = (characterIndex + 1) % 2;
        currentEventIndex = (currentEventIndex + 1) % onCharacterChangeEvent.Length;

        charactersImage[characterIndex].color = blacknedColor;
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
        onDialogFinish?.Invoke();
    }
}
