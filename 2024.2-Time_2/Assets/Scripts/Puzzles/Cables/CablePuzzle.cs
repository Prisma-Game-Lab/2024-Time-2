using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CablePuzzle : MonoBehaviour
{
    [SerializeField] private bool noite = true;
    [SerializeField] private bool charged = false;
    [SerializeField] private GameObject outline;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject linked;

    private void OnMouseDown()
    {
        Spin(gameObject);
        if (noite && linked != null)
        {
            Spin(linked);
        }
    }

    private void Spin(GameObject gameObject)
    {
        if (gameObject.transform.localPosition.x == 0)
        {
            gameObject.transform.localPosition = new Vector3(1, 0, 0);
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
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
