using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private UnityEvent interactionEffect;

    private void OnEnable()
    {
        PlayerController.onInteraction += RealizeInteraction;
    }

    private void OnDisable()
    {
        PlayerController.onInteraction -= RealizeInteraction;
    }

    void RealizeInteraction(GameObject objectInteracted) 
    {
        if(gameObject != gameObject) 
        {
            return;
        }
        interactionEffect.Invoke();
    }
}
