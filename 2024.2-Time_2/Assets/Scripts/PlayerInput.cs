using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Camera mainCamera;

    private GameObject draggingObject;
    private SpriteRenderer draggingObjectSR;
    private Vector2 difference;
    private bool isDragging;

    private Vector2 mousePosition;
    [SerializeField] private int draggingObjectOrder;
    private int defaultOrder = 0;

    public delegate void OnObjectGrab(GameObject grabbedObject);
    public static event OnObjectGrab onObjectGrab;

    public delegate void OnObjectDrop(GameObject droppedObject);
    public static event OnObjectDrop onObjectDrop;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            draggingObject.transform.position = mousePosition + difference;
        }
    }

    public void OnMousePress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider2D col = Physics2D.OverlapPoint(mousePosition);
            if (col != null)
            {
                if(draggingObjectSR != null)
                {
                    draggingObjectSR.sortingOrder = defaultOrder;
                }
                draggingObject = col.gameObject;
                difference = (Vector2)(draggingObject.transform.position) - mousePosition;
                isDragging = true;
                draggingObjectSR = draggingObject.GetComponent<SpriteRenderer>();
                defaultOrder = draggingObjectSR.sortingOrder;
                draggingObjectSR.sortingOrder = draggingObjectOrder;
                onObjectGrab(draggingObject);
            }
        }
        else if (context.canceled)
        {
            onObjectDrop(draggingObject);
            draggingObject = null;
            difference = Vector2.zero;
            isDragging = false;
        }
    }

    public void ReadMousePosition(InputAction.CallbackContext context) 
    {
        Vector2 screenMousePosition = context.ReadValue<Vector2>();
        mousePosition = mainCamera.ScreenToWorldPoint(screenMousePosition);
    }
}
