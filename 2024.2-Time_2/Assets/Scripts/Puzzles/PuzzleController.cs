using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public delegate void OnPuzzleStart();
    public static event OnPuzzleStart onPuzzleStart;

    public delegate void OnPuzzleEnd();
    public static event OnPuzzleEnd onPuzzleEnd;

    private void OnEnable()
    {
        onPuzzleStart?.Invoke();
    }

    private void OnDisable()
    {
        onPuzzleEnd?.Invoke();
    }
}
