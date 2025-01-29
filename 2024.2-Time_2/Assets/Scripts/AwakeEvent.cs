using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AwakeEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onStart;

    void Start()
    {
        StartCoroutine(FrameDelay());
    }

    IEnumerator FrameDelay() 
    {
        yield return new WaitForSeconds(0.1f);
        onStart.Invoke();
    } 
}
