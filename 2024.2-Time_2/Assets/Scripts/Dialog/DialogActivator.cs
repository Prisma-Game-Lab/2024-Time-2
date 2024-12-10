using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogActivator : MonoBehaviour
{
    private DialogController dialogController;

    [SerializeField] private string txtFileName;
    //[SerializeField] private UnityEvent[] onCharacterChange;
    [SerializeField] private UnityEvent onDialogEnd;
    [SerializeField] private Image[] charactersImage;
    [SerializeField] private string[] charactersNames;

    private void Awake()
    {
        dialogController = GameObject.FindWithTag("DialogController").GetComponent<DialogController>();
    }

    public void ActivateDialog()
    {
        dialogController.StartDialog(txtFileName, ref onDialogEnd, ref charactersImage, ref charactersNames);
    }
}
