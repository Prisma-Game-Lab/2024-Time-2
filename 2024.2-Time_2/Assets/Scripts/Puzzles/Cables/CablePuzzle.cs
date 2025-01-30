using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CablePuzzle : MonoBehaviour
{
    [SerializeField] private bool noite = true;
    [SerializeField] private bool charged = false;
    [SerializeField] private bool font = false;
    private float gambiarra;
    private bool gambiarra2;
    [SerializeField] public bool prism = false;
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;
    [SerializeField] private GameObject before;
    [SerializeField] private GameObject crossed;
    [SerializeField] private GameObject[] linked;

    private CablesManager cm;

    private void Awake()
    {
        cm = transform.parent.parent.GetComponent<CablesManager>();
    }

    private void OnMouseDown()
    {
        Spin(gameObject);
        for (int i = 0; i < linked.Length; i++)
        {
            Spin(linked[i]);
        }
    }

    public void Spin(GameObject objeto)
    {
        if (objeto.GetComponent<SpriteRenderer>().flipX)
        {
            objeto.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            objeto.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void Update()
    {
        if (charged)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = on;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = off;
        }
        if (!prism)
        {
            if (before != null)
            {
                if (before.GetComponent<CablePuzzle>().charged)
                {
                    charged = (gameObject.GetComponent<SpriteRenderer>().flipX == before.GetComponent<SpriteRenderer>().flipX);
                    if (font)
                    {
                        charged = true;
                        if (gambiarra < 1) 
                        {
                            gambiarra += Time.deltaTime;
                        }
                        else if (!gambiarra2)
                        {
                            cm.AddFont();
                            gambiarra2 = true;
                        }
                    }
                }
                else
                {
                    charged = false;
                    if (font && gambiarra2)
                    {
                        cm.SubtractFont();
                        gambiarra2 = false;
                        gambiarra = 0;
                    }
                }
                if (before.GetComponent<CablePuzzle>().font == true)
                {
                    charged = true;
                }
            }
        }
        else if (noite)
        {
            if (crossed != null)
            {
                if (crossed.GetComponent<CablePuzzle>().charged)
                {
                    charged = (gameObject.GetComponent<SpriteRenderer>().flipX == crossed.GetComponent<SpriteRenderer>().flipX);
                    if (font)
                    {
                        charged = true;
                        if (gambiarra < 1)
                        {
                            gambiarra += Time.deltaTime;
                        }
                        else if (gambiarra2)
                        {
                            cm.AddFont();
                            gambiarra2 = true;
                        }
                    }
                }
                else
                {
                    charged = false;
                    if (font && gambiarra2)
                    {
                        cm.SubtractFont();
                        gambiarra2 = false;
                        gambiarra = 0;
                    }
                }
                if (crossed.GetComponent<CablePuzzle>().font == true)
                {
                    charged = true;
                }
            }
        }
    }
}
