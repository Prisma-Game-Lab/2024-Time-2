using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogActivator : MonoBehaviour
{
    private DialogController dialogController;

    [TextArea(5, 14)]
    [SerializeField] private string[] characterText;
    [SerializeField] private string[] characterName;

    [SerializeField] private UnityEvent[] onCharacterChange;
    [SerializeField] private UnityEvent onDialogEnd;

    private void Awake()
    {
        dialogController = GameObject.FindWithTag("DialogController").GetComponent<DialogController>();
    }

    public void ActivateDialog()
    {
        dialogController.StartDialog(characterText, characterName, onCharacterChange, onDialogEnd);
    }
}
