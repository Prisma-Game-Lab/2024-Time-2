using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private UnityEvent interactionEffect;

    private void OnEnable()
    {
        PlayerController.onInteractionStart += RealizeInteraction;
    }

    private void OnDisable()
    {
        PlayerController.onInteractionStart -= RealizeInteraction;
    }

    void RealizeInteraction(GameObject objectInteracted) 
    {
        if(gameObject != objectInteracted) 
        {
            return;
        }
        interactionEffect.Invoke();
    }
}
