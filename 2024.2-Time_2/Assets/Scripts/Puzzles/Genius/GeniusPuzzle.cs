using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GeniusPuzzle : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool noite = false; //comecei como true por motivos de teste, mudar depois

    [Header("Stats")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private float turnAcceleration;
    [SerializeField] private int buttonsAmount;
    [SerializeField] private GeniusButtons[] buttonsControllers;
    [SerializeField] private UnityEvent onPuzzleCompleted;

    private List<int> solucao = new List<int>();
    private int nCorrectResponses = 0;
    private bool canAnswer = false;
    private bool onFailingSequence = false;
    private Color original;
    private int randomNum;

    private void Awake()
    {
        foreach (GeniusButtons geniusButtons in buttonsControllers)
        {
            geniusButtons.OnInitialization();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Glow());
    }

    private void OnDisable()
    {
        foreach (GeniusButtons geniusButtons in buttonsControllers)
        {
            geniusButtons.RevertToOriginalColor();
        }
    }

    private IEnumerator Glow()
    {
        nCorrectResponses = 0;
        solucao.Clear();
        canAnswer = false;

        foreach (GeniusButtons geniusButtons in buttonsControllers) 
        {
            geniusButtons.DisableInteraction();
        }

        yield return new WaitForSeconds(1);

        for (int i = 0; i < buttonsAmount; i++)
        {
            randomNum = Random.Range(0, buttonsControllers.Length);
            solucao.Add(randomNum);
            buttonsControllers[randomNum].StartGlow();
            yield return new WaitForSeconds(1);
            buttonsControllers[randomNum].RevertToOriginalColor();
            yield return new WaitForSeconds(0.5f);
        }

        canAnswer = true;
        foreach (GeniusButtons geniusButtons in buttonsControllers)
        {
            geniusButtons.EnableInteraction();
        }
    }

    public void OnColorPress(int colorID) 
    {
        if (!canAnswer)
        {
            return;
        }
        if(colorID == solucao[nCorrectResponses]) 
        {
            nCorrectResponses++;
            if (nCorrectResponses == buttonsAmount)
            {
                onPuzzleCompleted?.Invoke();
                //ganhou
            }
        }
        else 
        {
            StartCoroutine(OnIncorrectSequence());
            //perdeu
        }
    }

    private IEnumerator OnIncorrectSequence() 
    {
        if (!onFailingSequence) 
        {
            onFailingSequence = true;

            foreach (GeniusButtons geniusButtons in buttonsControllers)
            {
                geniusButtons.StartIncorrectGlow();
            }

            yield return new WaitForSeconds(1.5f);

            foreach (GeniusButtons geniusButtons in buttonsControllers)
            {
                geniusButtons.RevertToOriginalColor();
            }

            onFailingSequence = false;
        }

        StartCoroutine(Glow());
    }

    private void FixedUpdate()
    {
        if(noite == true)
        {
            transform.RotateAround(transform.position, Vector3.forward, turnSpeed + turnAcceleration * nCorrectResponses);
        }
    }
}
