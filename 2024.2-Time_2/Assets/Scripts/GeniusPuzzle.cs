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
    [SerializeField] private Image[] buttonsImages;
    [SerializeField] private UnityEvent onPuzzleCompleted;

    private List<int> solucao = new List<int>();
    private int nCorrectResponses = 0;
    private bool canAnswer = false;
    private Color original;
    private int randomNum;

    void OnEnable()
    {
        StartCoroutine(Glow());
    }

    void OnDisable() 
    {
        buttonsImages[randomNum].color = original;
    }

    IEnumerator Glow()
    {
        nCorrectResponses = 0;
        solucao.Clear();
        canAnswer = false;
        for (int i = 0; i < buttonsAmount; i++)
        {
            randomNum = Random.Range(0, buttonsImages.Length);
            solucao.Add(randomNum);
            original = buttonsImages[randomNum].color;
            buttonsImages[randomNum].color = new Color(255, 255, 255);
            yield return new WaitForSeconds(1);
            buttonsImages[randomNum].color = original;
            yield return new WaitForSeconds(1);
        }
        canAnswer = true;
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
            StartCoroutine(Glow());
            //perdeu
        }
    }

    void FixedUpdate()
    {
        if(noite == true)
        {
            transform.RotateAround(transform.position, Vector3.forward, turnSpeed + turnAcceleration * nCorrectResponses);
        }
    }
}
