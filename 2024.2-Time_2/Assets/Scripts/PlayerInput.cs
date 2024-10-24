using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject draggingObject;

    private Vector2 mousePosition;

    public delegate void OnMouseClick(GameObject grabbedObject);
    public static event OnMouseClick onMouseClick;

    public delegate void OnMouseCancelled(GameObject droppedObject);
    public static event OnMouseCancelled onMouseCancelled;

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
                onMouseClick?.Invoke(draggingObject);
            }
            else 
            {
                onMouseClick(null);
            }
        }
        else if (context.canceled)
        {
            if (draggingObject != null)
            {
                onMouseCancelled?.Invoke(draggingObject);
            }
            //draggingObject = null;
        }
    }

    public void ReadMousePosition(InputAction.CallbackContext context) 
    {
        Vector2 screenMousePosition = context.ReadValue<Vector2>();
        if(mainCamera != null) 
        {
            mousePosition = mainCamera.ScreenToWorldPoint(screenMousePosition);
        }
        GameManager.Instance.mousePos = mousePosition;
    }
}
