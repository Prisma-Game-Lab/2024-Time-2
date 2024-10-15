using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTo : MonoBehaviour
{
    [SerializeField] private GameObject[] snapablePlaces;

    [SerializeField] private float snapRange;

    private void OnEnable()
    {
        PlayerInput.onObjectDrop += ObjectSnap;
    }

    private void OnDisable()
    {
        PlayerInput.onObjectDrop -= ObjectSnap;
    }

    public void ObjectSnap(GameObject eventObject) 
    {
        if (eventObject != gameObject)
        {
            return;
        }
        int arrayLength = snapablePlaces.Length;
        if (arrayLength == 0)
        {
            print("ERROR H: No SnapablePlace Selected");
            return;
        }
        int minIndex = -1;
        float minDist = snapRange;
        float distanceToObject;
        for (int i = 0; i < arrayLength; i++) 
        {
            distanceToObject = (gameObject.transform.position - snapablePlaces[i].transform.position).magnitude;
            if (distanceToObject < snapRange && distanceToObject < minDist) 
            {
                minDist = distanceToObject;
                minIndex = i;
            }
        }
        if (minIndex != -1)
        {
            SnapPointController snapPointCon = snapablePlaces[minIndex].GetComponent<SnapPointController>();
            if (snapPointCon != null)
            {
                if (snapPointCon.SnapTest(gameObject)) 
                {
                    gameObject.transform.position = snapablePlaces[minIndex].transform.position;
                }
            }
            else 
            {
                print("ERROR H: No SnapPointController Script on object");
            }
        }
    }
}
