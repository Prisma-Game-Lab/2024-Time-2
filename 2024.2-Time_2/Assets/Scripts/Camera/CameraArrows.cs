using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArrows : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject leftArrow, rightArrow;

    public void TurnOnLeftArrow() 
    {
        leftArrow.SetActive(true);
    }

    public void TurnOnRightArrow()
    {
        rightArrow.SetActive(true);
    }

    public void TurnOffArrows() 
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }

    public void OnArrowClick(int i) 
    {
        cameraController.SetPosition(i);
    }
}
