using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public static int maxPrism = 1;
    public static int currPrism = 0;

    [SerializeField] private int objectiveCount = 0;

    public bool onDialog = false;

    public Vector2 mousePos;
    public Vector2 screenMousePosition;
    public int DragableObjectOrder = 0;

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

    public bool CheckObjective(int objectiveNumber) 
    {
        return objectiveCount >= objectiveNumber;
    }

    public void ChangeObjective(int objectiveNumber)
    {
        if (objectiveCount < objectiveNumber) 
        {
            objectiveCount = objectiveNumber;
        }
    }

    public void AddObjective()
    {
        objectiveCount++;
    }

    public IEnumerator WaitForTransition() 
    {
        yield return new WaitForSeconds(1f);
        onDialog = false;
    }
}
