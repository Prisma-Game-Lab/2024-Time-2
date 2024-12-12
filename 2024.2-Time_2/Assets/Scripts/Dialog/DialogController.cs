using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEngine.UI;
using System.Text;

public class DialogController : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private Color blacknedColor;
    private Dictionary<string, Image> characterImages = new Dictionary<string, Image>();

    [SerializeField] private float timeBetweenLetters;

    //private UnityEvent[] onCharacterChangeEvent;
    private UnityEvent onDialogEnd;

    public delegate void OnDialogStart();
    public static event OnDialogStart onDialogStart;

    public delegate void OnDialogFinish();
    public static event OnDialogFinish onDialogFinish;

    int currentIndex;
    //int currentEventIndex;
    int currentDialogSize;
    string[] fileLines;
    string currentName;

    private bool onDialog;
    private bool writingLine;

    public void StartDialog(string txtFileName, ref UnityEvent onDialogEndEvent, ref Image[] chrImages, ref string[] chrNames)
    {
        string txtPath;

        if (onDialog)
        {
            return;
        }

        onDialogStart?.Invoke();

        txtPath = Application.streamingAssetsPath + "/Dialogues/" + txtFileName + ".txt";
        fileLines = File.ReadAllLines(txtPath,Encoding.UTF8);

        currentDialogSize = fileLines.Length;
        //onCharacterChangeEvent = characterChangeEvents;
        onDialogEnd = onDialogEndEvent;
        dialogBox.SetActive(true);
        currentName = "";
        currentIndex = 0;
        //currentEventIndex = 0;

        characterImages.Clear();
        for (int i = 0; i < chrImages.Length; i++)
        {
            chrImages[i].gameObject.SetActive(true);
            chrImages[i].color = blacknedColor;
            characterImages.Add(chrNames[i], chrImages[i]);
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
                OnCharacterChange(newName);
            }
            StartCoroutine(TypeSentence());
        }
        else
        {
            EndDialog();
        }
    }

    private void OnCharacterChange(string newName)
    {
        speakerNameText.text = newName;
        characterImages[newName].color = new Color(255,255,255);
        if (characterImages.ContainsKey(currentName))
        {
            characterImages[currentName].color = blacknedColor;
        }
        currentName = newName;

        //onCharacterChangeEvent[currentEventIndex]?.Invoke();
        //currentEventIndex = (currentEventIndex + 1) % onCharacterChangeEvent.Length;
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
        foreach (Image image in characterImages.Values)
        {
            image.gameObject.SetActive(false);
        }
        onDialog = false;
        onDialogFinish?.Invoke();
        onDialogEnd?.Invoke();
    }
}
