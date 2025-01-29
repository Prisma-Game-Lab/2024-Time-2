using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStorage : MonoBehaviour
{
    public static PuzzleStorage Instance { get; private set; }

    public bool[] puzzlesDone = new bool[3];

    private int nPuzzlesDone;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SavePuzzle(int id) 
    {
        puzzlesDone[id] = true;
        nPuzzlesDone++;
    }

    public bool CheckPuzzle(int id) 
    {
        return puzzlesDone[id];
    } 

    public void DestroyPuzzleStorage() 
    {
        Destroy(gameObject);
    }
}
