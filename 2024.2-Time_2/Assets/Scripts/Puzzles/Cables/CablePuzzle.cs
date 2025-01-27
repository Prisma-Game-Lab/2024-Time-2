using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CablePuzzle : MonoBehaviour
{
    [SerializeField] private bool noite = true;
    [SerializeField] private bool charged = false;
    [SerializeField] private bool font = false;
    [SerializeField] public bool prism = false;
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;
    [SerializeField] private GameObject before;
    [SerializeField] private GameObject crossed;
    [SerializeField] private GameObject[] linked;


    private void OnMouseDown()
    {
        Spin(gameObject);
        if (noite)
        {
            for (int i = 0; i < linked.Length; i++)
            {
                Spin(linked[i]);
            }
        }
    }

    private void Spin(GameObject gameObject)
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
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
                    }
                }
                else
                {
                    charged = false;
                }
                if (before.GetComponent<CablePuzzle>().font == true)
                {
                    charged = true;
                }
            }
        }
        else
        {
            if (crossed != null)
            {
                if (crossed.GetComponent<CablePuzzle>().charged)
                {
                    charged = (gameObject.GetComponent<SpriteRenderer>().flipX == crossed.GetComponent<SpriteRenderer>().flipX);
                    if (font)
                    {
                        charged = true;
                    }
                }
                else
                {
                    charged = false;
                }
                if (crossed.GetComponent<CablePuzzle>().font == true)
                {
                    charged = true;
                }
            }
        }
    }
}
