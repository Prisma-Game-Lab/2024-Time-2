using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CablesManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onPuzzleCompleted;
    [SerializeField] private int maxFonts;
    private int fonts;

    public void AddFont() 
    {
        fonts += 1;
        if (fonts == maxFonts)
        {
            onPuzzleCompleted?.Invoke();
        }
    }

    public void SubtractFont()
    {
        fonts -= 1;
    }
}
