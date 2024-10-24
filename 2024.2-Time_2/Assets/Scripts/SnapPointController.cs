using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPointController : MonoBehaviour
{
    public GameObject snappedGameObject;

    [SerializeField] private float snapRange;
    [SerializeField] private LayerMask snapableObjects;

    public delegate void OnObjectInSnapChange();
    public static event OnObjectInSnapChange onObjectInSnapChange;

    private void OnEnable()
    {
        PlayerInput.onMouseCancelled += SnapTest;
    }

    private void OnDisable()
    {
        PlayerInput.onMouseCancelled -= SnapTest;
        PlayerInput.onMouseClick -= RemoveSnappedObject;
    }

    public void SnapTest(GameObject snapTestObject) 
    {
        if(snappedGameObject != null)
        {
            return;
        }

        //Collider2D[] hitResult = Physics2D.OverlapCircleAll(transform.position, snapRange, snapableObjects);
        //int HitSize = hitResult.Length;

        //if(HitSize <= 0) 
        //{
        //    return;
        //}

        //float distance = (gameObject.transform.position -hitResult[0].transform.position).magnitude;
        //float minDist = distance;
        //int minDistIndex = 0;

        //for (int i = 1; i < hitResult.Length; i++)
        //{
        //    distance = (gameObject.transform.position - hitResult[i].transform.position).magnitude;
        //    if (distance < minDist)
        //    {
        //        minDist = distance;
        //        minDistIndex = i;
        //    }
        //}

        if((snapTestObject.transform.position - transform.position).magnitude < snapRange) 
        {
            snappedGameObject = snapTestObject;
            snappedGameObject.transform.position = transform.position;
            PlayerInput.onMouseClick += RemoveSnappedObject;
            onObjectInSnapChange?.Invoke();
        }

        //snappedGameObject = hitResult[minDistIndex].gameObject;
        //snappedGameObject.transform.position = transform.position;
        //PlayerInput.onMouseClick += RemoveSnappedObject;
        //onObjectInSnapChange?.Invoke();
    } 

    private void RemoveSnappedObject(GameObject grabbedObject) 
    {
        if (snappedGameObject == grabbedObject)
        {
            snappedGameObject = null;
            PlayerInput.onMouseClick -= RemoveSnappedObject;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, snapRange);
    }
}
