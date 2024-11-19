using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeniusButtons : MonoBehaviour
{
    [SerializeField] private GameObject buttonObject;
    [SerializeField] private GameObject buttonPanel;

    Image buttonImage;
    Button buttonComponent;

    Color originalColor;

    public void OnInitialization()
    {
        buttonImage = buttonObject.GetComponent<Image>();
        originalColor = buttonImage.color;
        buttonComponent = buttonObject.GetComponent<Button>();
    }

    public void StartGlow()
    {
        buttonImage.color = new Color(255, 255, 255);
    }
    public void StartIncorrectGlow()
    {
        buttonImage.color = new Color(0, 0, 0);
    }

    public void RevertToOriginalColor() 
    {
        buttonImage.color = originalColor;
    }

    public void EnableInteraction() 
    {
        buttonComponent.enabled = true;
        buttonPanel.SetActive(true);
    }

    public void DisableInteraction()
    {
        buttonComponent.enabled = false;
        buttonPanel.SetActive(false);
    }

}