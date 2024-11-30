using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraMaxSpeed;
    [SerializeField] private float cameraAcceleration;
    [SerializeField] private float cameraDesacceleration;

    private float cameraCurrentSpeed;
    private int speedDirection;
    private bool shouldMove;
    private bool canMove;

    private void OnEnable()
    {
        PlayerInput.onMouseMove += CheckMousePos;

        LevelManager.onSceneTransition += DisableCameraMovement;
        LevelManager.onSceneTransitionEnd += EnableCameraMovement;

        DialogController.onDialogStart += DisableCameraMovement;
        DialogController.onDialogFinish += EnableCameraMovement;

        PuzzleController.onPuzzleStart += DisableCameraMovement;
        PuzzleController.onPuzzleEnd += EnableCameraMovement;
    }

    private void OnDisable()
    {
        PlayerInput.onMouseMove -= CheckMousePos;

        LevelManager.onSceneTransition -= DisableCameraMovement;
        LevelManager.onSceneTransitionEnd -= EnableCameraMovement;

        DialogController.onDialogStart -= DisableCameraMovement;
        DialogController.onDialogFinish -= EnableCameraMovement;

        PuzzleController.onPuzzleStart -= DisableCameraMovement;
        PuzzleController.onPuzzleEnd -= EnableCameraMovement;
    }

    private void FixedUpdate()
    {
        if (shouldMove && canMove) 
        {
            ApplyForces();
        }
    }

    private void CheckMousePos(float mousePositionX)
    {
        if (mousePositionX < Screen.width * 0.15f)
        {
            speedDirection = - 1;
            shouldMove = true;
        }
        else if (mousePositionX > Screen.width * 0.85f)
        {
            speedDirection = 1;
            shouldMove = true;
        }
        else 
        {
            speedDirection = 0;
        }
    }

    private void ApplyForces() 
    {
        if (speedDirection != 0) 
        {
            cameraCurrentSpeed += cameraAcceleration * speedDirection * Time.deltaTime;
            if (math.abs(cameraCurrentSpeed) > cameraMaxSpeed)
            {
                cameraCurrentSpeed = cameraMaxSpeed * speedDirection;
            }
        }
        else
        {
            if (cameraCurrentSpeed < 0) 
            {
                cameraCurrentSpeed += cameraDesacceleration * Time.deltaTime;
            }
            else if(cameraCurrentSpeed > 0)
            {
                cameraCurrentSpeed -= cameraDesacceleration * Time.deltaTime;
            }
            if (-0.5f < cameraCurrentSpeed && cameraCurrentSpeed < 0.5f)
            {
                cameraCurrentSpeed = 0;
                shouldMove = false;
                return;
            }
        }
        transform.position += new Vector3(cameraCurrentSpeed * Time.deltaTime, 0);
    }

    private void DisableCameraMovement()
    {
        cameraCurrentSpeed = 0;
        canMove = false;
    }

    private void EnableCameraMovement() 
    {
        canMove = true;
    }
}
