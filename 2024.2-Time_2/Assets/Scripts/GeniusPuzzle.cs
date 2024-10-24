using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeniusPuzzle : MonoBehaviour
{
    private List<int> solucao = new List<int> { };
    public static List<int> resposta = new List<int> { };

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            solucao.Add(UnityEngine.Random.Range(1, 4));
            Debug.Log(solucao[i]);
        }
    }

    void Update()
    {
        if (resposta.Count == 4)
        {
            if (Enumerable.SequenceEqual(solucao, resposta))
            {
                Debug.Log("ganhou");
                //ganhou
            }
            else
            {
                Debug.Log("perdeu");
                //perdeu
            }
        }
    }
}
