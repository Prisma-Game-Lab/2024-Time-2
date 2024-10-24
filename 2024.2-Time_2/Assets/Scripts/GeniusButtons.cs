using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GeniusButtons : MonoBehaviour
{
    [SerializeField] private int colorID;

    void OnMouseDown()
    {
        GeniusPuzzle.resposta.Add(colorID);
    }
}