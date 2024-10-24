using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float goalPos;
    private float distanceToGoal;
    private GameObject interactionGoal;

    private float currentMoveSpeed;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float smoothTime;

    [SerializeField] private float interactionRange;

    bool shouldMove;

    public delegate void OnInteraction(GameObject droppedObject);
    public static event OnInteraction onInteraction;

    private void OnEnable()
    {
        PlayerInput.onMouseClick += ChangeGoal;
    }

    private void OnDisable()
    {
        PlayerInput.onMouseClick -= ChangeGoal;
    }

    void FixedUpdate()
    {
        if (shouldMove) 
        {
            transform.position = new Vector2(Mathf.SmoothDamp(transform.position.x, goalPos, ref currentMoveSpeed, smoothTime, maxMoveSpeed),transform.position.y);
            distanceToGoal = transform.position.x - goalPos;
            if (Mathf.Abs(distanceToGoal) < 0.01f)
            {
                shouldMove = false;
            }
            else if (Mathf.Abs(distanceToGoal) < interactionRange) 
            {
                if (TriggerInteraction())
                {
                    shouldMove = false;
                }
            }
        }
    }

    void ChangeGoal(GameObject gameObject) 
    {
        goalPos = GameManager.Instance.mousePos.x;
        distanceToGoal = transform.position.x - goalPos;
        if (Mathf.Abs(distanceToGoal) > 0.01f) 
        {
            shouldMove = true;
        }
        interactionGoal = gameObject;
        if (Mathf.Abs(distanceToGoal) < interactionRange)
        {
            if (TriggerInteraction())
            {
                shouldMove = false;
            }
        }
    }

    bool TriggerInteraction() 
    {
        if (interactionGoal != null)
        {
            onInteraction?.Invoke(interactionGoal);
            return true;
        }
        return false;
    }
}
