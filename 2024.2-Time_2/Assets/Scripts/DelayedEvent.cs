using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onDelayEnd;
    [SerializeField] private float delayTime;

    public void WaitForDelay() 
    {
        StartCoroutine(delayMethod());
    }
    
    IEnumerator delayMethod() 
    {
        yield return new WaitForSeconds(delayTime);
        onDelayEnd.Invoke();
    }
}
