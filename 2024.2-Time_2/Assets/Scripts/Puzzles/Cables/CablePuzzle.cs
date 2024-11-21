using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CablePuzzle : MonoBehaviour
{
    [SerializeField] private bool charged = false;
    [SerializeField] private GameObject outline;
    [SerializeField] private GameObject left;

    private void OnMouseDown()
    {
        if (transform.localPosition.x == 0)
        {
            transform.localPosition = new Vector3(1, 0, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Update()
    {
        outline.SetActive(charged);
        if (left.GetComponent<CablePuzzle>().charged)
        {
            charged = (transform.localPosition == left.transform.localPosition);
        }
        else
        {
            charged = false;
        }
    }
}
