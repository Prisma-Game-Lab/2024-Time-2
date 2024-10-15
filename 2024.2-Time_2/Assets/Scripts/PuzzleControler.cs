using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PuzzleControler;

public class PuzzleControler : MonoBehaviour
{
    [SerializeField] private SnapPointController[] answerLocation;
    [SerializeField] private GameObject[] puzzleSolution;

    private void Awake()
    {
        if (answerLocation.Length != puzzleSolution.Length)
        {
            print("ERROR H: Different numbers of Answers Locations and Puzzle Solutions");
        }
    }

    private void OnEnable()
    {
        SnapPointController.onObjectInSnapChange += testSolution;
    }

    private void OnDisable()
    {
        SnapPointController.onObjectInSnapChange -= testSolution;
    }

    private void testSolution() 
    {
        for (int i = 0; i < answerLocation.Length; i++) 
        {
            if(answerLocation[i].snappedGameObject != puzzleSolution[i]) 
            {
                print(i);
                print("Wrong Solution");
                return;
            }
        }
        print("Right Solution!!!");
    }
}
