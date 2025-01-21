using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveReader : MonoBehaviour
{
    [SerializeField] private int objectiveNumber;
    [SerializeField] private UnityEvent onCorrectObjective;

    private void Awake()
    {
        if (GameManager.Instance.CheckObjective(objectiveNumber)) 
        {
            onCorrectObjective.Invoke();
        }
    }
}
