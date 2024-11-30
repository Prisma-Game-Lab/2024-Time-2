using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    bool changingScenes = true;

    public delegate void OnSceneTransition();
    public static event OnSceneTransition onSceneTransition;

    public delegate void OnSceneTransitionEnd();
    public static event OnSceneTransitionEnd onSceneTransitionEnd;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        StartCoroutine(inicialTransition());
    }

    public IEnumerator inicialTransition()
    {
        yield return new WaitForSeconds(1);
        changingScenes = false;
        onSceneTransitionEnd?.Invoke();
    }

    public void changeScene(string sceneName)
    {
        if (!changingScenes) 
        {
            StartCoroutine(waitForTransition(sceneName));
        }
    }

    public IEnumerator waitForTransition(string sceneName) 
    {
        changingScenes = true;
        onSceneTransition?.Invoke();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(1);
        changingScenes = false;
        onSceneTransitionEnd?.Invoke();
    }
}
