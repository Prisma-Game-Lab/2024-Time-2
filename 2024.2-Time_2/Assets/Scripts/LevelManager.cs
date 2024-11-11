using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    bool changingScenes;

    public delegate void OnSceneTransition();
    public static event OnSceneTransition onSceneTransition;
    
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
    }
}
