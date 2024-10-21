using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        PlayerInput.onObjectDrop += SnapTest;
    }

    private void OnDisable()
    {
        PlayerInput.onObjectDrop -= SnapTest;
        PlayerInput.onObjectGrab -= RemoveSnappedObject;
    }

    public void SnapTest(GameObject snapTestObject) 
    {
        if(snappedGameObject != null)
        {
            return;
        }

        Collider2D[] hitResult = Physics2D.OverlapCircleAll(transform.position, snapRange, snapableObjects);
        int HitSize = hitResult.Length;

        if(HitSize <= 0) 
        {
            return;
        }

        float distance = (gameObject.transform.position -hitResult[0].transform.position).magnitude;
        float minDist = distance;
        int minDistIndex = 0;

        for (int i = 1; i < hitResult.Length; i++)
        {
            distance = (gameObject.transform.position - hitResult[i].transform.position).magnitude;
            if (distance < minDist)
            {
                minDist = distance;
                minDistIndex = i;
            }
        }

        snappedGameObject = hitResult[minDistIndex].gameObject;
        snappedGameObject.transform.position = transform.position;
        PlayerInput.onObjectGrab += RemoveSnappedObject;
        onObjectInSnapChange();
    } 

    private void RemoveSnappedObject(GameObject grabbedObject) 
    {
        if (snappedGameObject == grabbedObject)
        {
            snappedGameObject = null;
            PlayerInput.onObjectGrab -= RemoveSnappedObject;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, snapRange);
    }
}
