using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour

{
    [SerializeField] GameObject pauseMenu;
    Boolean isGamePaused = false;
    // Start is called before the first frame update
    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }
    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    public void Menu(string sceneName){
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
    public void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        if (isGamePaused)
        {
            Resume();
            
        } else
        {
            Pause();
            
        }
    }
}
}
