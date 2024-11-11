using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    private Animator an;

    private void Awake() 
    {
        an = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        LevelManager.onSceneTransition += StartFadeOut;
    }

    private void OnDisable()
    {
        LevelManager.onSceneTransition -= StartFadeOut;
    }

    private void StartFadeOut() 
    {
        an.SetTrigger("ChangeScene");
    }
}
