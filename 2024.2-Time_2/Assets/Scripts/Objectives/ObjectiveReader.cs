using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveReader : MonoBehaviour
{
    [SerializeField] private bool dontCheckOnStart;
    [SerializeField] private bool puzzleMarker;
    [SerializeField] private int idNumber;
    [SerializeField] private int objectiveNumber;
    [SerializeField] private UnityEvent onCorrectObjective;

    private void Start()
    {
        if (dontCheckOnStart) 
        {
            return;
        }
        if (puzzleMarker) 
        {
            if (!PuzzleStorage.Instance.CheckPuzzle(idNumber)) 
            {
                checkObjective();
            }
            return;
        }
        checkObjective();
    }

    public void checkObjective() 
    {
        if (GameManager.Instance.CheckObjective(objectiveNumber))
        {
            onCorrectObjective.Invoke();
        }
    }
}
