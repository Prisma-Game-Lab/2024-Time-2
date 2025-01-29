using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerController pc;

    private Camera mainCamera;
    private GameObject clickedObject;

    private Vector2 mousePosition;
    private Vector2 mouseScreenPosition;

    [SerializeField] private float arrowSize;
    [SerializeField] private float arrowOffset;

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
            if (CheckForCameraArrows()) 
            {
                return;
            }
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
        mouseScreenPosition = context.ReadValue<Vector2>();
        if(mainCamera != null) 
        {
            mousePosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        }
        GameManager.Instance.mousePos = mousePosition;
    }

    private bool CheckForCameraArrows() 
    {
        if (Screen.height / 2 - arrowSize <= mouseScreenPosition.y && mouseScreenPosition.y <= Screen.height / 2 + arrowSize) 
        {
            if (mouseScreenPosition.x + arrowOffset <= arrowSize * 2 || Screen.width - arrowSize * 2 - arrowOffset <= mouseScreenPosition.x)
            {
                return true;
            }
        }
        return false;
    }
}
