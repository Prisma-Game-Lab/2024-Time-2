using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject draggingObject;

    private Vector2 mousePosition;

    public delegate void OnObjectGrab(GameObject grabbedObject);
    public static event OnObjectGrab onObjectGrab;

    public delegate void OnObjectDrop(GameObject droppedObject);
    public static event OnObjectDrop onObjectDrop;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnMousePress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider2D col = Physics2D.OverlapPoint(mousePosition);
            if (col != null)
            {
                draggingObject = col.gameObject;
                onObjectGrab(draggingObject);
            }
        }
        else if (context.canceled)
        {
            onObjectDrop(draggingObject);
            draggingObject = null;
        }
    }

    public void ReadMousePosition(InputAction.CallbackContext context) 
    {
        Vector2 screenMousePosition = context.ReadValue<Vector2>();
        mousePosition = mainCamera.ScreenToWorldPoint(screenMousePosition);
        GameManager.Instance.mousePos = mousePosition;
    }
}
