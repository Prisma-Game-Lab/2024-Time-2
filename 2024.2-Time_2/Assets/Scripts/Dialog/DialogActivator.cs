using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogActivator : MonoBehaviour
{
    private DialogController dialogController;

    [SerializeField] private string txtFileName;
    [SerializeField] private UnityEvent[] onCharacterChange;
    [SerializeField] private UnityEvent onDialogEnd;
    [SerializeField] private Sprite[] characterSprites;
    [SerializeField] private bool firstSpeaker;

    private void Awake()
    {
        dialogController = GameObject.FindWithTag("DialogController").GetComponent<DialogController>();
    }

    public void ActivateDialog()
    {
        dialogController.StartDialog(txtFileName, onCharacterChange, onDialogEnd, characterSprites, firstSpeaker);
    }
}
