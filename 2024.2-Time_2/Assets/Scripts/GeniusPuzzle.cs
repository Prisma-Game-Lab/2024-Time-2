using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GeniusPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject center;
    [SerializeField] private GameObject red;
    [SerializeField] private GameObject yellow;
    [SerializeField] private GameObject blue;
    [SerializeField] private GameObject green;
    private List<int> solucao = new List<int> { };
    public static List<int> resposta = new List<int> { };
    private List<GameObject> buttons = new List<GameObject> { };

    void Start()
    {
        buttons.Add(red);
        buttons.Add(yellow);
        buttons.Add(blue);
        buttons.Add(green);
        StartCoroutine(Glow());
    }

    IEnumerator Glow()
    {
        for (int i = 0; i < 4; i++)
        {
            int num = UnityEngine.Random.Range(1, 4);
            solucao.Add(num);
            Debug.Log(solucao[i]);
            for (int j = 0; j < 4; j++)
            {
                if (num == buttons[j].GetComponent<GeniusButtons>().colorID)
                {
                    //mudar essa forma de trocar a cor pra piscar porque algumas nao tao trocando, rgb e daltonismo meu (?)
                    UnityEngine.Color original = buttons[i].GetComponent<Renderer>().material.color;
                    buttons[j].GetComponent<Renderer>().material.color = new UnityEngine.Color(100, 100, 100);
                    yield return new WaitForSeconds(3);
                    buttons[j].GetComponent<Renderer>().material.color = original;
                    yield return new WaitForSeconds(1);
                }
            }
        }
    }

    void Update()
    {
        transform.RotateAround(center.transform.position, Vector3.forward, 0.5f);
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
