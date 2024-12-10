using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform childCamera;
    [SerializeField] private Transform[] cameraPossiblePositions;
    [SerializeField] private int starterPositionIndex;
    [SerializeField] private float smoothTime; 
    private CameraArrows cameraArrows;
    private int nPossiblePositions;
    private Vector2 desiredPosition;
    private Vector2 velocity = Vector2.zero;

    private int curentPositionIndex;

    private float cameraCurrentSpeed;
    private int speedDirection;
    private bool shouldMove;
    private bool canMove;

    private void Awake()
    {
        cameraArrows = GameObject.Find("CameraArrows").GetComponent<CameraArrows>();
        cameraArrows.SetUp(this);

        nPossiblePositions = cameraPossiblePositions.Length;
        curentPositionIndex = 0;

        curentPositionIndex = starterPositionIndex;
        childCamera.position = cameraPossiblePositions[starterPositionIndex].position;
        DisplayArrows();
    }

    private void OnEnable()
    {
        LevelManager.onSceneTransition += DisableCameraMovement;
        LevelManager.onSceneTransitionEnd += EnableCameraMovement;

        DialogController.onDialogStart += DisableCameraMovement;
        DialogController.onDialogFinish += EnableCameraMovement;

        PuzzleController.onPuzzleStart += DisableCameraMovement;
        PuzzleController.onPuzzleEnd += EnableCameraMovement;
    }

    private void OnDisable()
    {
        LevelManager.onSceneTransition -= DisableCameraMovement;
        LevelManager.onSceneTransitionEnd -= EnableCameraMovement;

        DialogController.onDialogStart -= DisableCameraMovement;
        DialogController.onDialogFinish -= EnableCameraMovement;

        PuzzleController.onPuzzleStart -= DisableCameraMovement;
        PuzzleController.onPuzzleEnd -= EnableCameraMovement;
    }

    private void FixedUpdate()
    {
        if (shouldMove)
        {
            childCamera.position = Vector2.SmoothDamp(childCamera.position, desiredPosition, ref velocity, smoothTime);
            if (Vector2.Distance(childCamera.position, desiredPosition) < 0.1f) 
            {
                shouldMove = false;
                if (canMove) 
                {
                    DisplayArrows();
                }
            }
        }
    }

    private void DisplayArrows() 
    {
        if (curentPositionIndex > 0) 
        {
            cameraArrows.TurnOnLeftArrow();
        }
        if (curentPositionIndex < nPossiblePositions - 1) 
        {
            cameraArrows.TurnOnRightArrow();
        }
    }

    public void SetPosition(int i) 
    {
        curentPositionIndex += i;
        desiredPosition = cameraPossiblePositions[curentPositionIndex].position;
        shouldMove = true;
        cameraArrows.TurnOffArrows();
    }

    private void DisableCameraMovement()
    {
        canMove = false;
        cameraArrows.TurnOffArrows();
    }

    private void EnableCameraMovement() 
    {
        canMove = true;
        if (!shouldMove)
        {
            DisplayArrows();
        }
    }
}
