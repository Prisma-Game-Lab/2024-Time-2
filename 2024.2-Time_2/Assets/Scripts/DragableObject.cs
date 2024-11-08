using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    private SpriteRenderer sr;
    private PlayerInput playerInput;

    private Vector2 difference;

    private bool beingDragged;

    public delegate void OnDragStart(GameObject droppedObject);
    public static event OnDragStart onDragStart;

    public delegate void OnDragEnd(GameObject droppedObject);
    public static event OnDragEnd onDragEnd;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        PlayerController.onInteractionStart += StartDrag;
    }

    private void OnDisable()
    {
        PlayerController.onInteractionStart -= StartDrag;
        PlayerController.onInteractionEnd -= EndDrag;
    }

    private void FixedUpdate()
    {
        if (beingDragged)
        {
            transform.position = GameManager.Instance.mousePos + difference;
        }
    }

    private void StartDrag(GameObject grabbedObject)
    {
        if(grabbedObject != gameObject) 
        {
            return;
        }
        beingDragged = true;
        sr.sortingOrder = GameManager.Instance.DragableObjectOrder;
        PlayerController.onInteractionEnd += EndDrag;
        difference = (Vector2)transform.position - GameManager.Instance.mousePos;
        onDragStart?.Invoke(gameObject);
    }

    private void EndDrag(GameObject droppedObject) 
    {
        beingDragged = false;
        GameManager.Instance.DragableObjectOrder += 1;
        PlayerController.onInteractionEnd -= EndDrag;
        onDragEnd?.Invoke(gameObject);
    }
}
