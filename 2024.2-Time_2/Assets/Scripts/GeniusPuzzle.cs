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
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject victory; //marcador de vitoria temporario
    [SerializeField] private GameObject red;
    [SerializeField] private GameObject yellow;
    [SerializeField] private GameObject blue;
    [SerializeField] private GameObject green;

    [SerializeField] private float turnSpeed;

    private List<int> solucao = new List<int> { };
    public static List<int> resposta = new List<int> { };
    private List<GameObject> buttons = new List<GameObject> { };
    public static int comeco = 0;
    public static bool noite = false; //comecei como true por motivos de teste, mudar depois

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
                    UnityEngine.Color original = buttons[j].GetComponent<SpriteRenderer>().color;
                    buttons[j].GetComponent<SpriteRenderer>().color = new UnityEngine.Color(255, 255, 255);
                    yield return new WaitForSeconds(1);
                    buttons[j].GetComponent<SpriteRenderer>().color = original;
                    yield return new WaitForSeconds(1);
                }
            }
            comeco = 1;
        }
    }

    void FixedUpdate()
    {
        if(noite == true)
        {
            transform.RotateAround(center.transform.position, Vector3.forward, turnSpeed + 0.75f * resposta.Count);
        }
        if (resposta.Count == 4)
        {
            if (Enumerable.SequenceEqual(solucao, resposta))
            {
                StartCoroutine(Win());
                Debug.Log("ganhou");
                //ganhou
            }
            else
            {
                solucao.Clear();
                resposta.Clear();
                buttons.Clear();
                comeco = 0;
                Start();
                Debug.Log("perdeu");
                //perdeu
            }
        }
    }

    IEnumerator Win()
    {
        victory.SetActive(true);
        yield return new WaitForSeconds(5);
        victory.SetActive(false);
        popup.SetActive(false);
    }
}
