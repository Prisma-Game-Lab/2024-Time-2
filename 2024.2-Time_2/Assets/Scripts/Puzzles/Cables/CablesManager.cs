using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CablesManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onPuzzleCompleted;
    public static int fonts;
    [SerializeField] private int maxFonts;

    private void Update()
    {
        if (fonts == maxFonts)
        {
            fonts = 0;
            onPuzzleCompleted?.Invoke();
        }
    }
}
