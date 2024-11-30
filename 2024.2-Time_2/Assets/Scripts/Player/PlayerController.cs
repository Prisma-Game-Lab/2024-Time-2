using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool interacting;

    public delegate void OnInteractionStart(GameObject droppedObject);
    public static event OnInteractionStart onInteractionStart;

    public delegate void OnInteractionEnd(GameObject droppedObject);
    public static event OnInteractionEnd onInteractionEnd;

    void FixedUpdate()
    {
        transform.position = Camera.main.ScreenToWorldPoint(GameManager.Instance.screenMousePosition);
        //if (shouldMove) 
        //{
        //    transform.position = new Vector2(Mathf.SmoothDamp(transform.position.x, goalPos, ref currentMoveSpeed, smoothTime, maxMoveSpeed),transform.position.y);
        //    distanceToGoal = transform.position.x - goalPos;
        //    if (Mathf.Abs(distanceToGoal) < 0.01f)
        //    {
        //        shouldMove = false;
        //    }
        //    else if (Mathf.Abs(distanceToGoal) < interactionRange) 
        //    {
        //        if (TriggerInteraction())
        //        {
        //            shouldMove = false;
        //        }
        //    }
        //}
    }

    //void ChangeGoal(GameObject gameObject) 
    //{
    //    //goalPos = GameManager.Instance.mousePos.x;
    //    //distanceToGoal = transform.position.x - goalPos;
    //    //if (Mathf.Abs(distanceToGoal) > 0.01f) 
    //    //{
    //    //    shouldMove = true;
    //    //}
    //    //interactionGoal = gameObject;
    //    //if (Mathf.Abs(distanceToGoal) < interactionRange)
    //    //{
    //    //    if (TriggerInteraction())
    //    //    {
    //    //        shouldMove = false;
    //    //    }
    //    //}
    //}

    public void StartTriggerInteraction(GameObject interactedObject) 
    {
        if (interactedObject != null)
        {
            onInteractionStart?.Invoke(interactedObject);
        }
    }

    public void CancelTriggerInteraction(GameObject interactedObject)
    {
        if (interactedObject != null)
        {
            onInteractionEnd?.Invoke(interactedObject);
        }
    }
}
