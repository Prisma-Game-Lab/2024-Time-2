using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPointController : MonoBehaviour
{
    public GameObject snappedGameObject;

    public delegate void OnObjectInSnapChange();
    public static event OnObjectInSnapChange onObjectInSnapChange;

    private void OnEnable()
    {
        PlayerInput.onObjectGrab += RemoveSnappedObject;
    }

    private void OnDisable()
    {
        PlayerInput.onObjectGrab -= RemoveSnappedObject;
    }

    public bool SnapTest(GameObject snapTestObject) 
    {
        if(snappedGameObject == null)
        {
            snappedGameObject = snapTestObject;
            onObjectInSnapChange();
            return true;
        }
        return false;
    } 

    private void RemoveSnappedObject(GameObject grabbedObject) 
    {
        if (snappedGameObject == grabbedObject)
        {
            snappedGameObject = null;
            onObjectInSnapChange();
        }
    }
}
