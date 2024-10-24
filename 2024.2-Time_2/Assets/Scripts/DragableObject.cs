using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    private SpriteRenderer sr;
    private PlayerInput playerInput;

    private Vector2 difference;

    private bool beingDragged;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        PlayerInput.onMouseClick += StartDrag;
    }

    private void OnDisable()
    {
        PlayerInput.onMouseClick -= StartDrag;
        PlayerInput.onMouseCancelled -= EndDrag;
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
        PlayerInput.onMouseCancelled += EndDrag;
        difference = (Vector2)(transform.position) - GameManager.Instance.mousePos;
    }

    private void EndDrag(GameObject droppedObject) 
    {
        beingDragged = false;
        GameManager.Instance.DragableObjectOrder += 1;
        PlayerInput.onMouseCancelled -= EndDrag;
    }
}
