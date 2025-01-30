using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CablePrism : MonoBehaviour
{
    [SerializeField] private bool state = false;
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;
    [SerializeField] private GameObject upR;
    [SerializeField] private GameObject downR;

    private void Start()
    {
        AlgumaCoisaLa();
    }

    private void OnMouseDown()
    {
        AlgumaCoisaLa();
    }

    private void AlgumaCoisaLa() 
    {
        if (state)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = off;
            GameManager.currPrism -= 1;
            upR.GetComponent<CablePuzzle>().prism = false;
            downR.GetComponent<CablePuzzle>().prism = false;
            state = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = on;
            GameManager.currPrism += 1;
            upR.GetComponent<CablePuzzle>().prism = true;
            downR.GetComponent<CablePuzzle>().prism = true;
            state = true;
        }
    }
}
