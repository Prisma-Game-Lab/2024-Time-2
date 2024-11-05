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

    private void Awake()
    {
        dialogController = GameObject.FindWithTag("DialogController").GetComponent<DialogController>();
    }

    public void ActivateDialog()
    {
        dialogController.StartDialog(txtFileName, onCharacterChange, onDialogEnd);
    }
}
