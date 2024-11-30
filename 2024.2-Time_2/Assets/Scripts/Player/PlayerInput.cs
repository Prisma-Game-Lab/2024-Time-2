using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public delegate void OnMouseMove(float mousePositionX);
    public static event OnMouseMove onMouseMove;

    private PlayerController pc;

    private Camera mainCamera;
    private GameObject clickedObject;

    private Vector2 mousePosition;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        if(pc == null) 
        {
            Debug.LogError("No PlayerController connected to PlayerInput");
        }
        mainCamera = Camera.main;
    }

    public void OnMousePress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider2D col = Physics2D.OverlapPoint(mousePosition);
            if (col != null)
            {
                clickedObject = col.gameObject;
                pc.StartTriggerInteraction(clickedObject);
            }
            else 
            {
                pc.StartTriggerInteraction(null);
            }
        }
        else if (context.canceled)
        {
            if (clickedObject != null)
            {
                pc.CancelTriggerInteraction(clickedObject);
                clickedObject = null;
            }
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
        onMouseMove(screenMousePosition.x);
    }
}
