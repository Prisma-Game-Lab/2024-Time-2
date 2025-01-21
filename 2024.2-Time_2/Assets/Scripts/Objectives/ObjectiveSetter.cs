using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSetter : MonoBehaviour
{
    [SerializeField] private int objectiveNumber;

    public void SetObjective() 
    {
        GameManager.Instance.ChangeObjective(objectiveNumber);
    }
}
